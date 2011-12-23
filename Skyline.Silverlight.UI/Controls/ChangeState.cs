using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Skyline.Silverlight.UI.Controls
{
    public class ChangeState : Control
    {
        public ChangeState()
        {
            this.DefaultStyleKey = typeof(ChangeState);
            IsHitTestVisible = false;
        }

        public object State
        {
            get { return (object)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(object), typeof(ChangeState), new PropertyMetadata(StateChanged));


        private static void StateChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            GoToStateAction(sender, e.NewValue);
        }


        public bool BooleanState
        {
            get { return (bool)GetValue(BooleanStateProperty); }
            set { SetValue(BooleanStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BooleanState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BooleanStateProperty =
            DependencyProperty.Register("BooleanState", typeof(bool), typeof(ChangeState), new PropertyMetadata(BooleanStateChanged));

        private static void BooleanStateChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            GoToBooleanStateAction(sender, e.NewValue);
        }


        public string TrueStateAction
        {
            get { return (string)GetValue(TrueStateActionProperty); }
            set { SetValue(TrueStateActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TrueStateAction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrueStateActionProperty =
            DependencyProperty.Register("TrueStateAction", typeof(string), typeof(ChangeState), null);



        public string FalseStateAction
        {
            get { return (string)GetValue(FalseStateActionProperty); }
            set { SetValue(FalseStateActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FalseStateAction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FalseStateActionProperty =
            DependencyProperty.Register("FalseStateAction", typeof(string), typeof(ChangeState), null);


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (State != null)
            {
                GoToStateAction(this, State);
            }
            else if (!string.IsNullOrEmpty(TrueStateAction) && ! string.IsNullOrEmpty(FalseStateAction))
            {
                GoToBooleanStateAction(this, BooleanState);
            }
        }


        private static void GoToStateAction(object sender, object newValue)
        {
            Control root = FindRootControl(sender);

            if (root != null && newValue != null)
            {
                VisualStateManager.GoToState(root, newValue.ToString(), true);
            }
        }

        private static void GoToBooleanStateAction(object sender, object newValue)
        {
            Control root = FindRootControl(sender);

            if (newValue != null && newValue is bool)
            {
                ChangeState thisControl = (ChangeState)sender;
                if ((bool)newValue)
                {
                    if (!string.IsNullOrEmpty(thisControl.TrueStateAction)) VisualStateManager.GoToState(root, thisControl.TrueStateAction, true);
                }
                else
                {
                    if (!string.IsNullOrEmpty(thisControl.FalseStateAction)) VisualStateManager.GoToState(root, thisControl.FalseStateAction, true);
                }
            }
        }


        /// <summary>
        /// Find first parent element that is a control, then we can apply the visual state manager
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private static Control FindRootControl(object sender)
        {           
            DependencyObject element = sender as DependencyObject;
            
            while (VisualTreeHelper.GetParent(element) != null)
            {
                element = VisualTreeHelper.GetParent(element);
                if (element is Control) return element as Control;
            }

            return null;
            //throw new Exception("Visual tree error");
        }



    }
}
