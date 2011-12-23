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

namespace Skyline.Silverlight.UI.Animations
{
    public class ScaleAnimation
    {
        private const string _scaleUpKey = "Skyline.Silverlight.UI.Animations.ScaleUp";
        private const string _scaleDownKey = "Skyline.Silverlight.UI.Animations.ScaleDown";

        private const double _scaleDuration = 0.5;

        public static void Show(UserControl userControl)
        {
            userControl.Visibility = Visibility.Visible;
        }

        public static void Hide(UserControl userControl)
        {
            userControl.Visibility = Visibility.Collapsed;
        }
    }
}
