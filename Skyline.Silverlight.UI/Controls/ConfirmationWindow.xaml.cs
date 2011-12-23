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
using Skyline.Silverlight.UI.ViewModels;

namespace Skyline.Silverlight.UI.Controls
{
    public partial class ConfirmationWindow : ChildWindow
    {
        public ConfirmationWindow()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (OkClicked != null) OkClicked(this, null);
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //this.TextBlockMessage.Text = _textMessage;
        }

        private string _textMessage;

        public string TextMessage
        {
            get { return _textMessage; }
            set 
            { 
                _textMessage = value;
                if (this.TextBlockMessage != null) this.TextBlockMessage.Text = _textMessage;
            }
        }

        public event RoutedEventHandler OkClicked;
    }
}

