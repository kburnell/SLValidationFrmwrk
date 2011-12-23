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
    public class AsyncCallCounter
    {
        private int _callCount = 0;

        public void Reset()
        {
            _callCount = 0;
        }

        public void Increment()
        {
            _callCount += 1;
            //System.Diagnostics.Debug.WriteLine("Inc: " + _callCount);
        }

        public void Decrement()
        {            
            if (_callCount == 0) return;

            if (_callCount == 1)
            {
                _callCount = 0;
                //System.Diagnostics.Debug.WriteLine("COMPLETE");
                if (CallsCompleted != null)
                {
                    CallsCompleted(this, null);
                }
            }
            else
            {
                _callCount -= 1;
                //System.Diagnostics.Debug.WriteLine("Dec: " + _callCount);
            }
        }

        public event EventHandler<EventArgs> CallsCompleted;
    }
}
