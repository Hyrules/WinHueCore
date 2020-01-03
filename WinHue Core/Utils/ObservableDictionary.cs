using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Linq;
using WinHue_Core.Philips_Hue;
using System.Net;
using System.Windows;
using System.ComponentModel;

namespace WinHue_Core.Utils
{
    public class ObservableDictionary<TKey,TItem> : Dictionary<TKey,TItem> , INotifyCollectionChanged, INotifyPropertyChanged
    {
        private bool _deferNotifyCollectionChanged = false;

        public new void Add(TKey key, TItem item)
        {
            if (base.Keys.Any(x => x.ToString() == key.ToString())) return;
            base.Add(key, item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey,TItem>(key,item)));
        }

        public void Add(Tuple<TKey,TItem> item)
        {
            Add(item.Item1, item.Item2);
        }

        public void Add(KeyValuePair<TKey,TItem> item)
        {
            Add(item.Key, item.Value);
        }

        public new bool Remove(TKey key)
        {
            if (!base.Keys.Any(x => x.ToString() == key.ToString())) return false;
            TItem item = base[key];
            base.Remove(key);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey,TItem>(key,item)));
            return true;
        }

        public new void Clear()
        {
            base.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, null));
        }

        public void AddRange(ObservableDictionary<TKey,TItem> coll)
        {
            _deferNotifyCollectionChanged = true;
            foreach (KeyValuePair<TKey, TItem> kvp in coll)
                Add(kvp.Key, kvp.Value);
            _deferNotifyCollectionChanged = false;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, null));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {

            if (_deferNotifyCollectionChanged) return;

            if(Application.Current == null)
            {
                NotifyChanges(e);
                return;
            }

            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(() => NotifyChanges(e));
            }
            else
                NotifyChanges(e);
            
        }


        private void NotifyChanges(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
        }
    }
}
