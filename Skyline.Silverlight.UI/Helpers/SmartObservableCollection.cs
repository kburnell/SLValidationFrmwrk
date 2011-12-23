using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Skyline.Silverlight.UI.Helpers
{
    public class SmartObservableCollection<T> : ObservableCollection<T>
    {
        private bool _disableNotifyCollectionChanged = false;

        public SmartObservableCollection()
            : base()
        {
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_disableNotifyCollectionChanged)
            {
                base.OnCollectionChanged(e);
            }
        }

        public void DisableNotifyCollectionChanged()
        {
            _disableNotifyCollectionChanged = true;
        }

        public void EnableNotifyCollectionChanged()
        {
            _disableNotifyCollectionChanged = false;
            var eventArg = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            this.OnCollectionChanged(eventArg);
        }

        public void AddItems(List<T> items)
        {
            this.DisableNotifyCollectionChanged();

            try
            {
                foreach (var item in items)
                {
                    base.InsertItem(base.Count, item);
                }
            }
            finally
            {
                this.EnableNotifyCollectionChanged();
            }
        }

        public void AddItems(IEnumerable<T> items)
        {
            this.DisableNotifyCollectionChanged();

            try
            {
                foreach (var item in items)
                {
                    base.InsertItem(base.Count, item);
                }
            }
            finally
            {
                this.EnableNotifyCollectionChanged();
            }
        }

    }
}
