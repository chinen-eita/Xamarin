using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Library.Data
{
    public static class LocalDataManager
    {
        /// <summary>
        /// ローカルデータアクセスオブジェクトを取得し、返却します。
        /// </summary>
        /// <param name="name">ローカルデータ接続先名</param>
        /// <returns>ローカルデータアクセスオブジェクト</returns>
        public static ILocalData LocalData(string name = "default")
        {
            // TODO
            return IpsalyzerApp.LocalData(name);
        }
        /// <summary>
        /// ローカルデータを読み取り、複数件のデータ読み取り結果を返却します。
        /// </summary>
        /// <param name="handler">データ読み取りハンドラ</param>
        /// <param name="defaultValue">デフォルト値</param>
        /// <param name="name">データ接続先名</param>
        /// <returns>データ読み取り結果</returns>
        public static IQueryable ReadLocal(Func<Realm, IQueryable> handler, IQueryable defaultValue = null, string name = "default")
        {
            return LocalDataManager.ReadLocal<IQueryable>(handler, defaultValue, name);
        }
        /// <summary>
        /// ローカルデータを読み取り、1件のデータ読み取り結果を返却します。
        /// </summary>
        /// <typeparam name="T">1件データの型</typeparam>
        /// <param name="handler">データ読み取りハンドラ</param>
        /// <param name="defaultValue">デフォルト値</param>
        /// <param name="name">データ接続先名</param>
        /// <returns>1件のデータ読み取り結果</returns>
        public static T ReadLocal<T>(Func<Realm, T> handler, T defaultValue = null, string name = "default") where T : class
        {
            return LocalDataManager.LocalData(name).Read<T>(handler, defaultValue);
        }
        /// <summary>
        /// データ書き込みハンドラを呼び出して、ローカルデータへ書き込みを行います。
        /// </summary>
        /// <remarks>
        /// データ書き込みハンドラがfalseを返却する、または例外をthrowした場合はロールバックが行われます。
        /// それ以外の場合は、コミットされます。
        /// </remarks>
        /// <param name="handler">データ書き込みハンドラ</param>
        /// <param name="name">データ接続先名</param>
        public static void WriteLocal(Func<Realm, bool?> handler, string name = "default")
        {
            LocalDataManager.LocalData(name).Write(handler);
        }
        /// <summary>
        /// データ書き込みハンドラを呼び出して、ローカルデータへ書き込みを行います。
        /// </summary>
        /// <remarks>
        /// データ書き込みハンドラが例外をthrowした場合はロールバックが行われます。
        /// それ以外の場合は、コミットされます。
        /// </remarks>
        /// <param name="handler">データ書き込みハンドラ</param>
        /// <param name="name">データ接続先名</param>
        public static void WriteLocal(Action<Realm> handler, string name = "default")
        {
            LocalDataManager.LocalData(name).Write(realm => {
                handler.Invoke(realm);
                return null;
            });
        }
        /// <summary>
        /// Realmデータベースに対して、1件データを登録し、処理結果を返却します。
        /// </summary>
        /// <typeparam name="T">エンティティ型</typeparam>
        /// <param name="realm">Realmデータベースオブジェクト</param>
        /// <param name="entity">登録エンティティ</param>
        /// <returns>処理が成功した場合はtrue。失敗した場合はfalse。</returns>
        public static bool? Insert<T>(Realm realm, T entity) where T : RealmObject
        {
            if (realm == null || entity == null)
            {
                return false;
            }
            realm.Add<T>(entity);
            return true;
        }
        /// <summary>
        /// Realmデータベースに対して、1件データを更新し、処理結果を返却します。
        /// </summary>
        /// <typeparam name="T">エンティティ型</typeparam>
        /// <param name="realm">Realmデータベースオブジェクト</param>
        /// <param name="entity">更新エンティティ</param>
        /// <returns>処理が成功した場合はtrue。失敗した場合はfalse。</returns>
        public static bool? Update<T>(Realm realm, T entity) where T : RealmObject
        {
            if (realm == null || entity == null)
            {
                return false;
            }
            realm.Add<T>(entity, true);
            return true;
        }
        /// <summary>
        /// Realmデータベースからデータを1件削除します。
        /// </summary>
        /// <typeparam name="T">エンティティ型</typeparam>
        /// <param name="realm">Realmデータベースオブジェクト</param>
        /// <param name="primaryKey">エンティティのプライマリキー</param>
        /// <returns>処理が成功した場合はtrue。失敗した場合はfalse。</returns>
        public static bool? Delete<T>(Realm realm, string primaryKey) where T : RealmObject
        {
            var entity = realm.Find<T>(primaryKey);
            if (entity == null)
            {
                return false;
            }
            return LocalDataManager.Delete<T>(realm, entity);
        }
        /// <summary>
        /// Realmデータベースからデータを1件削除します。
        /// </summary>
        /// <typeparam name="T">エンティティ型</typeparam>
        /// <param name="realm">Realmデータベースオブジェクト</param>
        /// <param name="entity">削除エンティティ</param>
        /// <returns>処理が成功した場合はtrue。失敗した場合はfalse。</returns>
        public static bool? Delete<T>(Realm realm, T entity) where T : RealmObject
        {
            if (realm == null || entity == null)
            {
                return false;
            }
            realm.Remove(entity);
            return true;
        }
    }
}
