using System;
using SilverlightValidationFramework.ViewModels.BaseClasses;

namespace SilverlightValidationFramework.Utility {

    public class ProeprtyValidator<TBaseViewModel> where TBaseViewModel : BaseViewModel<TBaseViewModel> {

        #region << Private Fields >>

        private readonly string _propertyName;
        private string _errorMessage;
        private Func<TBaseViewModel, bool> _validationCriteria;

        #endregion

        #region << Constructors >>

        public ProeprtyValidator(string propertyName) {
            _propertyName = propertyName;
        }

        #endregion

        #region << Public Properties >>

        public string PropertyName { get { return _propertyName; } }

        public bool IsInvalid(TBaseViewModel presentationModel) {
            if (_validationCriteria == null)
                throw new InvalidOperationException("Criteria must be provided");
            return _validationCriteria(presentationModel);
        }

        public string GetErrorMessage() {
            if (_errorMessage == null)
                throw new InvalidOperationException("Error message must be provided");
            return _errorMessage;
        }

        #endregion

        #region << Fluid Extensions >>

        public ProeprtyValidator<TBaseViewModel> When(Func<TBaseViewModel, bool> validationCriteria) {
            _validationCriteria = validationCriteria;
            return this;
        }

        public ProeprtyValidator<TBaseViewModel> Show(string errorMessage) {
            _errorMessage = errorMessage;
            return this;
        }

        #endregion
    }
}