using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Library.Data
{
    /// <summary>
    /// ローカルデータへアクセスするインターフェイスを定義します。
    /// </summary>
    public interface ILocalData : IDisposable
    {
        /// <summary>
        /// 生のRealmオブジェクトを取得し、返却します。
        /// </summary>
        /// <returns>生のRealmオブジェクト</returns>
        Realm Raw();
        /// <summary>
        /// 指定されたhandlerで読み取り処理を行い、結果を返却します。
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="handler">データ読み取り処理</param>
        /// <param name="defaultValue">データ不存在時のデフォルト値</param>
        /// <returns>取得されたデータ。存在しない場合はデフォルト値。</returns>
        T Read<T>(Func<Realm, T> handler, T defaultValue = null) where T : class;
        /// <summary>
        /// データ書き込みハンドラを呼び出して、ローカルデータへ書き込みを行います。
        /// </summary>
        /// <remarks>
        /// データ書き込みハンドラがfalseを返却する、または例外をthrowした場合はロールバックが行われます。
        /// それ以外の場合は、コミットされます。
        /// </remarks>
        /// <param name="handler">データ書き込みハンドラ</param>
        void Write(Func<Realm, bool?> handler);
    }
}
