using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Skyline.Silverlight.UI.Animations;

namespace Skyline.Silverlight.UI.Helpers
{
    public static class UserControlExtensions
    {
        public static void Show(this UserControl userControl)
        {
            FadeAnimation.Show(userControl);
        }

        public static void Hide(this UserControl userControl)
        {
            FadeAnimation.Hide(userControl);
        }



    }
}
