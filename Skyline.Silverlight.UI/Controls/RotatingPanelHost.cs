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
    public class RotatingPanelHost : ItemsControl
    {
        bool _firstTime = true;

        #region Constructor/Initialization

        public RotatingPanelHost()
        {
            this.DefaultStyleKey = typeof(RotatingPanelHost);
            this.SizeChanged += new SizeChangedEventHandler(RotatingPanelHost_SizeChanged);
            this.LayoutUpdated += new EventHandler(RotatingPanelHost_LayoutUpdated);
        }

        #endregion

        #region Dependency Properties



        #endregion

        #region Overrides

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            RotatingPanel panel = item as RotatingPanel;
            panel.OnSelected += new RoutedEventHandler(panel_OnSelected);
            _childPanels.Add(panel);
        }

        #endregion

        #region Control Events

        void RotatingPanelHost_LayoutUpdated(object sender, EventArgs e)
        {

        }

        void RotatingPanelHost_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newWidth = e.NewSize.Width;
            if (double.IsInfinity(newWidth) || double.IsNaN(newWidth) || newWidth == 0) return;

            double panelMargin = 50;
            int panelZIndex = _childPanels.Count;

            double left = this.Padding.Left;
            double width = this.ActualWidth - this.Padding.Left - this.Padding.Right - ((panelZIndex - 1) * panelMargin);

            foreach (RotatingPanel panel in _childPanels)
            {
                if (_firstTime)
                {
                    Canvas.SetZIndex(panel, panelZIndex--);
                    panel.Position = PositionStateEnum.Right;
                }
                Canvas.SetLeft(panel, left);
                Canvas.SetTop(panel, this.Padding.Top);
                panel.Width = width;
                panel.Height = this.ActualHeight - this.Padding.Top - this.Padding.Bottom;

                left += panelMargin;
            }

            if (_firstTime)
            {
                if (_childPanels.Count > 0) _childPanels[0].Position = PositionStateEnum.Center;
                _firstTime = false;
            }

        }

        void panel_OnSelected(object sender, RoutedEventArgs e)
        {
            RotatingPanel selectedPanel = sender as RotatingPanel;

            int zIndex = 0;
            bool isAfterSelectedPanel = false;
            foreach (RotatingPanel panel in _childPanels)
            {
                if (panel == selectedPanel)
                {
                    panel.Position = PositionStateEnum.Center;
                    zIndex = _childPanels.Count;
                    Canvas.SetZIndex(panel, zIndex);
                    isAfterSelectedPanel = true;
                }
                else if (isAfterSelectedPanel)
                {
                    panel.Position = PositionStateEnum.Right;
                    Canvas.SetZIndex(panel, --zIndex);
                }
                else
                {
                    panel.Position = PositionStateEnum.Left;
                    Canvas.SetZIndex(panel, zIndex++);
                }
            }
        }

        #endregion

        #region Helpers

        private List<RotatingPanel> _childPanels = new List<RotatingPanel>();


        #endregion
    }
}
