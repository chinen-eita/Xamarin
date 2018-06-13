using LMS.Library.Data;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LMS.Library.Base
{
    /// <summary>
    /// ViewModelクラスの基底クラスを定義します。
    /// </summary>
    /// <remarks>
    /// ViewModelでは、Viewクラスへのバインドプロパティ公開、バインドコマンド公開およびデータの読み取り・書き込みなど
    /// 業務ロジックの処理を記述します。
    /// </remarks>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// プロパティ変更時イベント
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// プロパティ変更時のイベント処理を行います。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        ///// <summary>
        ///// ローカルデータアクセスオブジェクトを取得し、返却します。
        ///// </summary>
        ///// <param name="name">ローカルデータ接続先名</param>
        ///// <returns>ローカルデータアクセスオブジェクト</returns>
        //public ILocalData LocalData(string name = "default")
        //{
        //    return IpsalyzerApp.LocalData(name);
        //}
        ///// <summary>
        ///// ローカルデータを読み取り、複数件のデータ読み取り結果を返却します。
        ///// </summary>
        ///// <param name="handler">データ読み取りハンドラ</param>
        ///// <param name="defaultValue">デフォルト値</param>
        ///// <param name="name">データ接続先名</param>
        ///// <returns>データ読み取り結果</returns>
        //public IQueryable ReadLocal(Func<Realm, IQueryable> handler, IQueryable defaultValue = null, string name = "default")
        //{
        //    return this.ReadLocal<IQueryable>(handler, defaultValue, name);
        //}
        ///// <summary>
        ///// ローカルデータを読み取り、1件のデータ読み取り結果を返却します。
        ///// </summary>
        ///// <typeparam name="T">1件データの型</typeparam>
        ///// <param name="handler">データ読み取りハンドラ</param>
        ///// <param name="defaultValue">デフォルト値</param>
        ///// <param name="name">データ接続先名</param>
        ///// <returns>1件のデータ読み取り結果</returns>
        //public T ReadLocal<T>(Func<Realm, T> handler, T defaultValue = null, string name = "default") where T : class
        //{
        //    return this.LocalData(name).Read<T>(handler, defaultValue);
        //}
        ///// <summary>
        ///// データ書き込みハンドラを呼び出して、ローカルデータへ書き込みを行います。
        ///// </summary>
        ///// <remarks>
        ///// データ書き込みハンドラがfalseを返却する、または例外をthrowした場合はロールバックが行われます。
        ///// それ以外の場合は、コミットされます。
        ///// </remarks>
        ///// <param name="handler">データ書き込みハンドラ</param>
        ///// <param name="name">データ接続先名</param>
        //public void WriteLocal(Func<Realm, bool?> handler, string name = "default")
        //{
        //    this.LocalData(name).Write(handler);
        //}
        ///// <summary>
        ///// データ書き込みハンドラを呼び出して、ローカルデータへ書き込みを行います。
        ///// </summary>
        ///// <remarks>
        ///// データ書き込みハンドラが例外をthrowした場合はロールバックが行われます。
        ///// それ以外の場合は、コミットされます。
        ///// </remarks>
        ///// <param name="handler">データ書き込みハンドラ</param>
        ///// <param name="name">データ接続先名</param>
        //public void WriteLocal(Action<Realm> handler, string name = "default")
        //{
        //    this.LocalData(name).Write(realm => {
        //        handler.Invoke(realm);
        //        return null;
        //    });
        //}
        ///// <summary>
        ///// Realmデータベースに対して、1件データを登録し、処理結果を返却します。
        ///// </summary>
        ///// <typeparam name="T">エンティティ型</typeparam>
        ///// <param name="realm">Realmデータベースオブジェクト</param>
        ///// <param name="entity">登録エンティティ</param>
        ///// <returns>処理が成功した場合はtrue。失敗した場合はfalse。</returns>
        //public bool? Insert<T>(Realm realm, T entity) where T : RealmObject
        //{
        //    if (realm == null || entity == null)
        //    {
        //        return false;
        //    }
        //    realm.Add<T>(entity);
        //    return true;
        //}
        ///// <summary>
        ///// Realmデータベースに対して、1件データを更新し、処理結果を返却します。
        ///// </summary>
        ///// <typeparam name="T">エンティティ型</typeparam>
        ///// <param name="realm">Realmデータベースオブジェクト</param>
        ///// <param name="entity">更新エンティティ</param>
        ///// <returns>処理が成功した場合はtrue。失敗した場合はfalse。</returns>
        //public bool? Update<T>(Realm realm, T entity) where T : RealmObject
        //{
        //    if (realm == null || entity == null)
        //    {
        //        return false;
        //    }
        //    realm.Add<T>(entity, true);
        //    return true;
        //}
        ///// <summary>
        ///// Realmデータベースからデータを1件削除します。
        ///// </summary>
        ///// <typeparam name="T">エンティティ型</typeparam>
        ///// <param name="realm">Realmデータベースオブジェクト</param>
        ///// <param name="primaryKey">エンティティのプライマリキー</param>
        ///// <returns>処理が成功した場合はtrue。失敗した場合はfalse。</returns>
        //public bool? Delete<T>(Realm realm, string primaryKey) where T : RealmObject
        //{
        //    var entity = realm.Find<T>(primaryKey);
        //    if (entity == null)
        //    {
        //        return false;
        //    }
        //    return this.Delete<T>(realm, entity);
        //}
        ///// <summary>
        ///// Realmデータベースからデータを1件削除します。
        ///// </summary>
        ///// <typeparam name="T">エンティティ型</typeparam>
        ///// <param name="realm">Realmデータベースオブジェクト</param>
        ///// <param name="entity">削除エンティティ</param>
        ///// <returns>処理が成功した場合はtrue。失敗した場合はfalse。</returns>
        //public bool? Delete<T>(Realm realm, T entity) where T : RealmObject
        //{
        //    if (realm == null || entity == null)
        //    {
        //        return false;
        //    }
        //    realm.Remove(entity);
        //    return true;
        //}


        /// <summary>
        /// 指定されたPageオブジェクトを表示します。
        /// </summary>
        /// <param name="page">ページオブジェクト</param>
        /// <param name="animated">アニメーションをする場合はtrue</param>
        /// <returns>Taskオブジェクト</returns>
        public Task ShowNextPage(Page page, bool animated = false)
        {
            return IpsalyzerApp.Of().ShowNextPage(page, animated);
        }
        /// <summary>
        /// 現在のページを閉じ、元の画面に戻ります。
        /// </summary>
        /// <param name="animated">アニメーションをする場合はtrue</param>
        /// <returns>Taskオブジェクト</returns>
        public Task CloseCurrentPage(bool animated = false)
        {
            return IpsalyzerApp.Of().CloseCurrentPage(animated);
        }
    }
}
