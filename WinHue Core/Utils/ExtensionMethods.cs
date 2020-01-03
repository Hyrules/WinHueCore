using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace WinHue_Core.Utils
{
    public static class ExtensionMethods
    {
        public static void AddRange<T>(this ICollection<T> coll, IEnumerable<T> newitems)
        {
            foreach (T element in newitems)
                coll.Add(element);

        }

    }
}
