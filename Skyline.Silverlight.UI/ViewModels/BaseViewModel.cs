using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Skyline.Silverlight.UI.ViewModels {

    public class BaseViewModel : INotifyPropertyChanged {

        public bool IsInDesignMode { get { return false; } } // return DesignerProperties.GetIsInDesignMode(Application.Current.RootVisual); } }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

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

        protected static string GetPropertyName<TProperty>(Expression<Func<TProperty>> property)
        {
            if (property == null)
                throw new ArgumentNullException("property");
            MemberExpression memberExpression;
            if (property.Body is UnaryExpression)
                memberExpression = ((UnaryExpression)property.Body).Operand as MemberExpression;
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

        #region Standard ViewModel Regions

        #region Constructor/Initialization

        #endregion

        #region Properties

        #endregion

        #region Events

        #endregion

        #region Public Methods

        #endregion

        #region Helpers

        #endregion

        #endregion
    }
}