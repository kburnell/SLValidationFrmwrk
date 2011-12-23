using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using SilverlightValidationFramework.Services.Data;
using SilverlightValidationFramework.ViewModels.BaseClasses;

namespace SilverlightValidationFramework.ViewModels {

    public class NewEmployeePageViewModel : BaseViewModel<NewEmployeePageViewModel> {

        #region << Private Fields >>

        private string _confirmPassword;
        private DateTime? _dateOfBirth;
        private string _firstName;
        private string _lastName;
        private string _password;
        private Division _division;
        private ObservableCollection<Division> _divisions = new ObservableCollection<Division> { new Division(), new Division { Id = 1, Title = "North" }, new Division { Id = 2, Title = "South" } };

        private const string DefaultRequiredFieldMessage = " Is Required";

        #endregion

        #region << Constructors/Initializers >>

        public new void Initialize() {
            base.Initialize();
            AddValidationFor(() => ConfirmPassword).When(x => !string.IsNullOrEmpty(x._password) && !x._password.Equals(x._confirmPassword)).Show("Confirm Password does not match Password");
        }

        #endregion

        #region << Public Properties >>

        [Display(Name = "First Name"), Required(ErrorMessage = DefaultRequiredFieldMessage)]
        public string FirstName {
            get { return _firstName; }
            set {
                if (_firstName != value) {
                    _firstName = value;
                    NotifyPropertyChanged(() => FirstName);
                }
            }
        }

        [Display(Name = "Last Name"), Required(ErrorMessage = DefaultRequiredFieldMessage)]
        public string LastName {
            get { return _lastName; }
            set {
                if (_lastName != value) {
                    _lastName = value;
                    NotifyPropertyChanged(() => LastName);
                }
            }
        }

        [Display(Name = "Date of Birth"), Required(ErrorMessage = " Is Required"), Range(typeof (DateTime), "1/1/1950", "12/31/2250")]
        public DateTime? DateOfBirth {
            get { return _dateOfBirth; }
            set {
                if (_dateOfBirth != value) {
                    _dateOfBirth = value;
                    NotifyPropertyChanged(() => DateOfBirth);
                }
            }
        }

        [Display(Name = "Password"), Required(ErrorMessage = DefaultRequiredFieldMessage), StringLength(12, MinimumLength = 6, ErrorMessage = " Must be between 6 and 12 characters long.")]
        public string Password {
            get { return _password; }
            set {
                if (_password != value) {
                    _password = value;
                    NotifyPropertyChanged(() => Password);
                }
            }
        }

        [Display(Name = "Confirm Password"), Required(ErrorMessage = DefaultRequiredFieldMessage)]
        public string ConfirmPassword {
            get { return _confirmPassword; }
            set {
                if (_confirmPassword != value) {
                    _confirmPassword = value;
                    NotifyPropertyChanged(() => ConfirmPassword);
                }
            }
        }

        public ObservableCollection<Division> Divisions {
            get { return _divisions; }
            set {
                if (_divisions != value) {
                    _divisions = value;
                    NotifyPropertyChanged(() => Divisions);
                }
            }
        }

        [Display(Name = "Division"), Required(ErrorMessage = DefaultRequiredFieldMessage)]
        public Division Division {
            get { return _division; }
            set {
                if (_division != value) {
                    _division = value;
                    NotifyPropertyChanged(() => Division);
                }
            }
        }

        #endregion

        #region << Events/Triggers >>

        public void Save() {
            ValidateAll();
            if (!HasErrors) {
                MessageBox.Show("New Employee Created!");
            }
        }

        public void Reset() {
            ClearAllValidationErrors();
        }

        #endregion

        #region << Private Methods >>

        #endregion
    }
}