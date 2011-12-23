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

namespace Skyline.Silverlight.UI.Helpers
{
    public static class FrameworkElementExtensions
    {
        public static void SetCanvasPosition(this FrameworkElement element, double left, double top)
        {
            Canvas.SetLeft(element, left);
            Canvas.SetTop(element, top);
        }

        public static void SetCanvasPosition(this FrameworkElement element, double left, double top, double width, double height)
        {
            Canvas.SetLeft(element, left);
            Canvas.SetTop(element, top);
            element.Width = width;
            element.Height = height;
        }



    }
}
