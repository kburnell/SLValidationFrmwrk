using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SilverlightValidationFramework.Utility;

namespace SilverlightValidationFramework.ViewModels.BaseClasses {

    public abstract class BaseViewModel<TBaseViewModel> : INotifyPropertyChanged, INotifyDataErrorInfo where TBaseViewModel : BaseViewModel<TBaseViewModel> {

        #region << Private Fields >>

        private readonly List<ProeprtyValidator<TBaseViewModel>> _validations = new List<ProeprtyValidator<TBaseViewModel>>();
        private Dictionary<string, List<string>> _errorMessages = new Dictionary<string, List<string>>();

        #endregion

        #region << Constructor >>

        protected BaseViewModel() {
            PropertyChanged += (s, e) => { if (e.PropertyName != "HasErrors") ValidateProperty(e.PropertyName); };
        }

        public virtual void Initialize() {
            HookupValidationAttributes();
        }

        #endregion

        #region << Public Methods >>

        public void ValidateAll() {
            Dictionary<string, List<string>>.KeyCollection propertyNamesWithValidationErrors = _errorMessages.Keys;
            _errorMessages = new Dictionary<string, List<string>>();
            _validations.ForEach(PerformValidation);
            List<string> propertyNamesThatMightHaveChangedValidation = _errorMessages.Keys.Union(propertyNamesWithValidationErrors).ToList();
            propertyNamesThatMightHaveChangedValidation.ForEach(OnErrorsChanged);
            NotifyPropertyChanged(() => HasErrors);
        }

        public void ValidateProperty<TProperty>(Expression<Func<TProperty>> expression) {
            ValidateProperty(GetPropertyName(expression));
        }

        public void ClearValidationErrors<TProperty>(Expression<Func<TProperty>> expression) {
            string propertyName = GetPropertyName(expression);
            _errorMessages.Remove(propertyName);
            OnErrorsChanged(propertyName);
            NotifyPropertyChanged(() => HasErrors);
        }

        public void ClearAllValidationErrors() {
            foreach (ProeprtyValidator<TBaseViewModel> propertyValidation in _validations) {
                _errorMessages.Remove(propertyValidation.PropertyName);
                OnErrorsChanged(propertyValidation.PropertyName);
            }
            NotifyPropertyChanged(() => HasErrors);
        }

        public void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void NotifyPropertyChanged<TProperty>(Expression<Func<TProperty>> property) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(GetPropertyName(property)));
            }
        }

        #endregion

        #region << Protected Methods >>

        protected ProeprtyValidator<TBaseViewModel> AddValidationFor(Expression<Func<object>> expression) {
            return AddValidationFor(GetPropertyName(expression));
        }

        protected ProeprtyValidator<TBaseViewModel> AddValidationFor(string propertyName) {
            ProeprtyValidator<TBaseViewModel> validation = new ProeprtyValidator<TBaseViewModel>(propertyName);
            _validations.Add(validation);
            return validation;
        }

        protected void HookupValidationAttributes() {
            PropertyInfo[] propertyInfos = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo propertyInfo in propertyInfos) {
                Attribute[] custom = Attribute.GetCustomAttributes(propertyInfo, typeof (ValidationAttribute), true);
                foreach (Attribute attribute in custom) {
                    PropertyInfo property = propertyInfo;
                    ValidationAttribute validationAttribute = attribute as ValidationAttribute;
                    if (validationAttribute == null) throw new NotSupportedException("validationAttribute variable should be inherited from ValidationAttribute type");
                    string name = property.Name;
                    DisplayAttribute displayAttribute = Attribute.GetCustomAttributes(propertyInfo, typeof (DisplayAttribute)).FirstOrDefault() as DisplayAttribute;
                    if (displayAttribute != null) {
                        name = displayAttribute.GetName();
                    }
                    string message = validationAttribute.FormatErrorMessage(name);
                    AddValidationFor(propertyInfo.Name).When(x => {
                                                                 object value = property.GetGetMethod().Invoke(this, new object[] {});
                                                                 ValidationResult result = validationAttribute.GetValidationResult(value, new ValidationContext(this, null, null) {MemberName = property.Name});
                                                                 return result != ValidationResult.Success;
                                                             }).Show(message);
                }
            }
        }

        protected static string GetPropertyName<TProperty>(Expression<Func<TProperty>> property) {
            if (property == null)
                throw new ArgumentNullException("property");
            MemberExpression memberExpression;
            if (property.Body is UnaryExpression)
                memberExpression = ((UnaryExpression) property.Body).Operand as MemberExpression;
            else
                memberExpression = property.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("The property is not a member access property", "property");
            PropertyInfo propInfo = memberExpression.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException("The member access expression does not access a property", "property");
            MethodInfo getMethod = propInfo.GetGetMethod(true);
            if (getMethod.IsStatic)
                throw new ArgumentException("The referenced property is a static property", "property");
            return memberExpression.Member.Name;
        }

        #endregion

        #region << Private Methods >>

        private void ValidateProperty(string propertyName) {
            _errorMessages.Remove(propertyName);
            _validations.Where(v => v.PropertyName == propertyName).ToList().ForEach(PerformValidation);
            OnErrorsChanged(propertyName);
            NotifyPropertyChanged(() => HasErrors);
        }

        private void PerformValidation(ProeprtyValidator<TBaseViewModel> validation) {
            if (validation.IsInvalid((TBaseViewModel) this)) {
                AddErrorMessageForProperty(validation.PropertyName, validation.GetErrorMessage());
            }
        }

        private void AddErrorMessageForProperty(string propertyName, string errorMessage) {
            if (_errorMessages.ContainsKey(propertyName)) {
                _errorMessages[propertyName].Add(errorMessage);
            }
            else {
                _errorMessages.Add(propertyName, new List<string> {errorMessage});
            }
        }

        #endregion

        #region << Implementation of INotifyDataErrorInfo >>

        public IEnumerable GetErrors(string propertyName) {
            if (_errorMessages.ContainsKey(propertyName))
                return _errorMessages[propertyName];

            return new string[0];
        }

        public bool HasErrors { get { return _errorMessages.Count > 0; } }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };

        private void OnErrorsChanged(string propertyName) {
            ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion

        #region << Implementation of INotifyPropertyChanged >>

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion
    }
}