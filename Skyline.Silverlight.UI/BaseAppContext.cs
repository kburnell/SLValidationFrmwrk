using System;
using System.IO.IsolatedStorage;
using System.Text;
using System.Windows;
using System.Windows.Browser;
using Skyline.Silverlight.UI.Controls;
using Skyline.Silverlight.UI.ViewModels;

namespace Skyline.Silverlight.UI {
    /// <summary>
    ///   This is meant to be a static class, it is declared normally with static methods and a private constructor so that it can be inherited by the static appcontext class in the application.
    /// </summary>
    public abstract class BaseAppContext {
        private static string _hostSiteURL;
        private static bool? _isSiteUsingSSL;

        public static string HostSiteURL {
            get {
                if (_hostSiteURL == null) {
                    string source = Application.Current.Host.Source.ToString();
                    int i = source.IndexOf("/ClientBin/");

                    string currentURL = source.Substring(0, i);

                    _hostSiteURL = currentURL;
                }
                return _hostSiteURL;
            }
        }

        public static bool IsSiteUsingSSL {
            get {
                if (!_isSiteUsingSSL.HasValue) {
                    _isSiteUsingSSL = HostSiteURL.ToLower().StartsWith("https");
                }

                return _isSiteUsingSSL.Value;
            }
        }

        public static void Initialize() {}

        public static void ShowValidationWindow(BaseViewModel viewModel) {
            ValidationErrorWindow errorWindow = new ValidationErrorWindow();

            // need to add an errors collection on the baseviewmodel that the error window is bound to hopefully using ValidationSummary control to show errors.

            errorWindow.DataContext = viewModel;
            errorWindow.Show();
        }

        public static void ShowMessageWindow(string message) {
            ShowMessageWindow("Message", message);
        }

        public static void ShowMessageWindow(string title, string message) {
            MessageWindow window = new MessageWindow();
            window.Title = title;
            window.TextMessage = message;

            window.Show();
        }

        public static void ShowErrorWindow(Exception e) {
            StringBuilder sb = new StringBuilder(500);
            string errorMsg = e.Message;

#if DEBUG

            errorMsg += e.StackTrace;

#endif

            errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

            ShowMessageWindow("Application Error", errorMsg);
        }

        public static void ShowNewBrowserWindow(string url) {
            HtmlPage.PopupWindow(new Uri(url), "new", null);
        }

        public static void SaveToISOStorage(string key, object data) {
            //IsolatedStorageSettings isolatedStorage = IsolatedStorageSettings.ApplicationSettings;

            //isolatedStorage[key] = data;
            //isolatedStorage.Save();
        }

        public static object LoadFromISOStorage(string key) {
            IsolatedStorageSettings isolatedStorage = IsolatedStorageSettings.ApplicationSettings;

            if (isolatedStorage.Contains(key)) return isolatedStorage[key];
            else return null;
        }

        public static void ClearISOStorage(string key) {
            IsolatedStorageSettings isolatedStorage = IsolatedStorageSettings.ApplicationSettings;

            isolatedStorage.Remove(key);
        }
    }
}