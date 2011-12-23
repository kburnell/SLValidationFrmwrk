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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Skyline.Silverlight.UI.Controls
{
    public class RotatingPanel : ContentControl
    {

        #region Constructor/Initialization

        public RotatingPanel()
        {
            this.DefaultStyleKey = typeof(RotatingPanel);
            this.Loaded += new RoutedEventHandler(RotatingPanel_Loaded);
            this.SizeChanged += new SizeChangedEventHandler(RotatingPanel_SizeChanged);
            this.LayoutUpdated += new EventHandler(RotatingPanel_LayoutUpdated);
        }

        void RotatingPanel_Loaded(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "NormalState", true);

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _textBlockCenterHeader = this.GetTemplateChild("TextBlockCenterHeader") as TextBlock;
            if (_textBlockCenterHeader != null)
            {
                _textBlockCenterHeader.Text = Header;
            }

            _textBlockCenterHeaderShadow = this.GetTemplateChild("TextBlockCenterHeaderShadow") as TextBlock;
            if (_textBlockCenterHeaderShadow != null)
            {
                _textBlockCenterHeaderShadow.Text = Header;
            }

            _textBlockLeftHeader = this.GetTemplateChild("TextBlockLeftHeader") as TextBlock;
            if (_textBlockLeftHeader != null)
            {
                _textBlockLeftHeader.Text = Header.ToUpper();
            }

            _textBlockRightHeader = this.GetTemplateChild("TextBlockRightHeader") as TextBlock;
            if (_textBlockRightHeader != null)
            {
                _textBlockRightHeader.Text = Header.ToUpper();
            }

            _borderOffCenterEffect = this.GetTemplateChild("BorderOffCenterEffect") as Border;
            if (_borderOffCenterEffect != null)
            {
                _borderOffCenterEffect.MouseLeftButtonUp += new MouseButtonEventHandler(_borderOffCenterEffect_MouseLeftButtonUp);
                _borderOffCenterEffect.MouseEnter += new MouseEventHandler(_borderOffCenterEffect_MouseEnter);
                _borderOffCenterEffect.MouseLeave += new MouseEventHandler(_borderOffCenterEffect_MouseLeave);
            }

            _borderBackground = this.GetTemplateChild("BorderBackground") as Border;
            _imageReflection = this.GetTemplateChild("ImageReflection") as Image;

        }



        #endregion

        #region Dependency Properties

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set
            {
                if (_textBlockCenterHeader != null) _textBlockCenterHeader.Text = value;
                if (_textBlockCenterHeaderShadow != null) _textBlockCenterHeaderShadow.Text = value;
                if (_textBlockLeftHeader != null) _textBlockLeftHeader.Text = value.ToUpper();
                if (_textBlockRightHeader != null) _textBlockRightHeader.Text = value.ToUpper();
                SetValue(HeaderProperty, value);
            }
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(RotatingPanel), null);


        public PositionStateEnum Position
        {
            get { return (PositionStateEnum)GetValue(PositionProperty); }
            set 
            {
                VisualStateManager.GoToState(this, value.ToString() + "State", true);

                SetValue(PositionProperty, value);
            }
        }

        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(PositionStateEnum), typeof(RotatingPanel), null);



        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty HeaderBackgroundProperty = DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(RotatingPanel), null);



        public Brush HeaderForeground
        {
            get { return (Brush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(RotatingPanel), null);




        public Brush HeaderForegroundShadow
        {
            get { return (Brush)GetValue(HeaderForegroundShadowProperty); }
            set { SetValue(HeaderForegroundShadowProperty, value); }
        }

        public static readonly DependencyProperty HeaderForegroundShadowProperty = DependencyProperty.Register("HeaderForegroundShadow", typeof(Brush), typeof(RotatingPanel), null);




        public Brush DisabledHeaderForeground
        {
            get { return (Brush)GetValue(DisabledHeaderForegroundProperty); }
            set { SetValue(DisabledHeaderForegroundProperty, value); }
        }

        public static readonly DependencyProperty DisabledHeaderForegroundProperty = DependencyProperty.Register("DisabledHeaderForeground", typeof(Brush), typeof(RotatingPanel), null);




        public Brush DisabledLeftHeaderBackground
        {
            get { return (Brush)GetValue(DisabledLeftHeaderBackgroundProperty); }
            set { SetValue(DisabledLeftHeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty DisabledLeftHeaderBackgroundProperty = DependencyProperty.Register("DisabledLeftHeaderBackground", typeof(Brush), typeof(RotatingPanel), null);



        public Brush DisabledRightHeaderBackground
        {
            get { return (Brush)GetValue(DisabledRightHeaderBackgroundProperty); }
            set { SetValue(DisabledRightHeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty DisabledRightHeaderBackgroundProperty = DependencyProperty.Register("DisabledRightHeaderBackground", typeof(Brush), typeof(RotatingPanel), null);





        #endregion

        #region Events

        public event RoutedEventHandler OnSelected;

        #endregion

        #region Control Events

        void RotatingPanel_LayoutUpdated(object sender, EventArgs e)
        {


        }

        void RotatingPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newWidth = e.NewSize.Width;
            if (double.IsInfinity(newWidth) || double.IsNaN(newWidth) || newWidth == 0) return;

            if (_imageReflection != null && _borderBackground != null)
            {
                WriteableBitmap bitmap = new WriteableBitmap(_borderBackground, null);
                _imageReflection.Source = bitmap;
            }

        }

        void _borderOffCenterEffect_MouseLeave(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "NormalState", true);
        }

        void _borderOffCenterEffect_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Position != PositionStateEnum.Center) VisualStateManager.GoToState(this, "MouseOverState", true); 
        }

        void _borderOffCenterEffect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OnSelected != null)
            {
                VisualStateManager.GoToState(this, "NormalState", true);
                OnSelected(this, null);
            }
        }

        #endregion

        #region Helpers

        private TextBlock _textBlockCenterHeader = null;
        private TextBlock _textBlockCenterHeaderShadow = null;
        private TextBlock _textBlockLeftHeader = null; 
        private TextBlock _textBlockRightHeader = null;
        private Border _borderBackground = null;
        private Border _borderOffCenterEffect = null;
        private Image _imageReflection = null;

        #endregion
    }

    public enum PositionStateEnum
    {
        Left,
        Center,
        Right
    }
}
