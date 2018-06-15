using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Library.Data
{
    /// <summary>
    /// ローカルデータへアクセスするインターフェイスを実装します。
    /// </summary>
    public class LocalData : ILocalData
    {
        /// <summary>
        /// 名前のRealmオブジェクト
        /// </summary>
        private Realm raw = null;
        /// <summary>
        /// ロックオブジェクト
        /// </summary>
        private volatile object locking = new object();

        #region public instance methods

        /// <summary>
        /// 名前のRealmオブジェクトを取得し、返却します。
        /// </summary>
        /// <returns>名前のRealmオブジェクト</returns>
        public Realm Raw()
        {
            lock (this.locking)
            {
                if (this.raw != null)
                {
                    return this.raw;
                }
                this.raw = Realm.GetInstance();
            }
            return this.raw;
        }

        /// <summary>
        /// 指定されたhandlerで読み取り処理を行い、結果を返却します。
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="handler">データ読み取り処理</param>
        /// <param name="defaultValue">データ不存在時のデフォルト値</param>
        /// <returns>取得されたデータ。存在しない場合はデフォルト値。</returns>
        public T Read<T>(Func<Realm, T> handler, T defaultValue = null) where T : class
        {
            if (handler == null)
            {
                return defaultValue;
            }
            var raw = this.Raw();
            try
            {
                var result = handler.Invoke(raw);
                return result != null ? result : defaultValue;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// データ書き込みハンドラを呼び出して、ローカルデータへ書き込みを行います。
        /// </summary>
        /// <remarks>
        /// データ書き込みハンドラがfalseを返却する、または例外をthrowした場合はロールバックが行われます。
        /// それ以外の場合は、コミットされます。
        /// </remarks>
        /// <param name="handler">データ書き込みハンドラ</param>
        public void Write(Func<Realm, bool?> handler)
        {
            if (handler == null)
            {
                return;
            }
            bool? result = null;
            var raw = this.Raw();
            var tx = raw.BeginWrite();
            try
            {
                result = handler.Invoke(raw);
                if (result.HasValue)
                {
                    if (result.Value == false)
                    {
                        tx.Rollback();
                        return;
                    }
                }
                tx.Commit();
            }
            catch
            {
                if (raw.IsInTransaction)
                {
                    tx.Rollback();
                }
                throw;
            }
        }

        /// <summary>
        /// リソース開放処理を行います。
        /// </summary>
        public void Dispose()
        {
            this.Raw().Dispose();
        }
        #endregion //end of public instance methods
    }
}
