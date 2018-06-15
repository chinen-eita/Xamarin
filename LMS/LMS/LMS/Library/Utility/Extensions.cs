using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Library.Utility
{
    /// <summary>
    /// 拡張メソッドによる機能を提供するクラスです。
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// ObservableCollectionとして返却します。
        /// </summary>
        /// <typeparam name="T">1件データの型</typeparam>
        /// <param name="target">対象クエリ結果</param>
        /// <returns>ObservableCollection</returns>
        public static ObservableCollection<T> AsObserveble<T>(this IQueryable<T> target)
        {
            if (target is ObservableCollection<T>)
                return target as ObservableCollection<T>;

            if (target != null)
                return new ObservableCollection<T>(target);
            else
                return new ObservableCollection<T>();
        }
    }
}
