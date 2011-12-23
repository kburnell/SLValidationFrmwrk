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
    public class FadeAnimation
    {
        private const string _fadeInKey = "Skyline.Silverlight.UI.Animations.FadeIn";
        private const string _fadeOutKey = "Skyline.Silverlight.UI.Animations.FadeOut";

        public static void Show(UserControl userControl)
        {
            Storyboard sb;

            userControl.Opacity = 0.0;
            userControl.Visibility = Visibility.Visible;

            if (!userControl.Resources.Contains(_fadeInKey))
            {
                DoubleAnimation fadeAnimation = new DoubleAnimation();
                fadeAnimation.BeginTime = TimeSpan.FromSeconds(0.10);
                fadeAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.20)); 
                fadeAnimation.From = 0.0;
                fadeAnimation.To = 1.0;

                sb = new Storyboard();
                sb.Duration = new Duration(TimeSpan.FromSeconds(0.30)); 
                sb.Children.Add(fadeAnimation);

                Storyboard.SetTarget(fadeAnimation, userControl);
                Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath("(Opacity)"));

                userControl.Resources.Add(_fadeInKey, sb);
            }
            else
            {
                sb = (Storyboard)userControl.Resources[_fadeInKey];
            }

            sb.Begin();
        }

        public static void Hide(UserControl userControl)
        {
            Storyboard sb;

            if (!userControl.Resources.Contains(_fadeOutKey))
            {
                Duration duration = new Duration(TimeSpan.FromSeconds(0.10));
                DoubleAnimation fadeAnimation = new DoubleAnimation();
                fadeAnimation.Duration = duration;
                fadeAnimation.From = 1.0;
                fadeAnimation.To = 0.0;

                ObjectAnimationUsingKeyFrames visibleAnimation = new ObjectAnimationUsingKeyFrames();
                DiscreteObjectKeyFrame keyframe1 = new DiscreteObjectKeyFrame();
                keyframe1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
                keyframe1.Value = Visibility.Visible;
                DiscreteObjectKeyFrame keyframe2 = new DiscreteObjectKeyFrame();
                keyframe2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.10));
                keyframe2.Value = Visibility.Collapsed;

                visibleAnimation.KeyFrames.Add(keyframe1);
                visibleAnimation.KeyFrames.Add(keyframe2);

                sb = new Storyboard();
                sb.Duration = duration;
                sb.Children.Add(fadeAnimation);
                sb.Children.Add(visibleAnimation);

                Storyboard.SetTarget(visibleAnimation, userControl);
                Storyboard.SetTargetProperty(visibleAnimation, new PropertyPath("Visibility"));

                Storyboard.SetTarget(fadeAnimation, userControl);
                Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath("(Opacity)"));

                userControl.Resources.Add(_fadeOutKey, sb);
            }
            else
            {
                sb = (Storyboard)userControl.Resources[_fadeOutKey];
            }

            sb.Begin();
        }
    }
}
