using LMS.Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LMS.Library.Data
{
    /// <summary>
    /// IPSALyzerアプリケーションクラスです。
    /// </summary>
    public class IpsalyzerApp
    {
        /// <summary>
        /// インスタンス生成解決ディクショナリ
        /// </summary>
        private Dictionary<string, Tuple<Func<IpsalyzerApp, Object>, bool>> resolverDictionary = new Dictionary<string, Tuple<Func<IpsalyzerApp, Object>, bool>>();
        /// <summary>
        /// インスタンスキャッシュディクショナリ
        /// </summary>
        private Dictionary<string, Object> instanceCache = new Dictionary<string, object>();
        /// <summary>
        /// アプリケーション終了時Disposeオブジェクトリスト
        /// </summary>
        private List<Object> finalizeList = new List<object>();
        /// <summary>
        /// アプリケーションインスタンス
        /// </summary>
        public static IpsalyzerApp instance = null;

        /// <summary>
        /// アプリケーション終了時の処理を行います。
        /// </summary>
        ~IpsalyzerApp()
        {
            var instance = Of();
            foreach (var current in instance.finalizeList)
            {
                try
                {
                    if (current is IDisposable)
                    {
                        (current as IDisposable).Dispose();
                    }
                }
                catch
                {

                }
            }
        }


        #region public static methods
        /// <summary>
        /// アプリケーション初期化時の処理を行います。
        /// </summary>
        /// <remarks>インスタンス生成方法をアプリケーションインスタンスに登録します。</remarks>
        /// <returns>Ipsalyzerアプリケーションインスタンス</returns>
        public static IpsalyzerApp Initialize()
        {
            var instance = Of();
            //TODO:接続先DBが複数になる場合やconfigを読み取るなどする場合など、用途に応じて
            //インスタンス生成方法を追加する。
            instance.Register<ILocalData>(ipsalyzer => new LocalData(), null, true);

            return instance;
        }
        /// <summary>
        /// アプリケーションインスタンスを返却します。
        /// </summary>
        /// <returns>アプリケーションインスタンス</returns>
        public static IpsalyzerApp Of()
        {
            if (instance != null)
            {
                return instance;
            }
            instance = new IpsalyzerApp();
            return instance;
        }
        /// <summary>
        /// ローカルデータアクセスオブジェクトを取得し、返却します。
        /// </summary>
        /// <param name="name">ローカルデータ接続先名</param>
        /// <returns>ローカルデータアクセスオブジェクト</returns>
        public static ILocalData LocalData(string name = "default")
        {
            return Of().Instance<ILocalData>(name, true);
        }
        #endregion //end of public static methods

        #region public instance methods
        #region Register methods
        /// <summary>
        /// 指定された型・インスタンス名に紐づくインスタンスを登録します。
        /// </summary>
        /// <typeparam name="T">登録する型名</typeparam>
        /// <param name="name">登録する型名の任意のインスタンス名</param>
        public void Register<T>(string name = "default") where T : new()
        {
            this.Register(typeof(T), ipsalyzer => new T(), name);
        }
        /// <summary>
        /// 指定された型・インスタンス名に紐づくインスタンスサプライヤ(インスタンス生成方法)を登録します。
        /// </summary>
        /// <typeparam name="T">登録する型名</typeparam>
        /// <param name="supplier">インスタンスサプライヤ</param>
        /// <param name="name">登録する型名の任意のインスタンス名</param>
        /// <param name="useCache">生成後にインスタンスをキャッシュする場合true</param>
        public void Register<T>(Func<IpsalyzerApp, T> supplier, string name = "default", bool useCache = false)
        {
            if (supplier == null)
            {
                return;
            }
            this.Register(typeof(T), ipsalyzer => supplier.Invoke(ipsalyzer), name, useCache);
        }
        /// <summary>
        /// 指定された型・インスタンス名に紐づくインスタンスサプライヤ(インスタンス生成方法)を登録します。
        /// </summary>
        /// <param name="type">登録する型名</param>
        /// <param name="supplier">インスタンスサプライヤ</param>
        /// <param name="name">登録する型名の任意のインスタンス名</param>
        /// <param name="useCache">生成後にインスタンスをキャッシュする場合true</param>
        public void Register(Type type, Func<IpsalyzerApp, Object> supplier, string name = "default", bool useCache = false)
        {
            if (type == null || supplier == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "default";
            }
            var id = this.GetInstanceId(type, name);
            this.Register(id, supplier, useCache);
        }
        /// <summary>
        /// 指定されたIDに紐づくインスタンスを登録します。
        /// </summary>
        /// <param name="TEAM_ID">インスタンスID</param>
        /// <param name="instance">インスタンスオブジェクト</param>
        public void Register(string id, Object instance)
        {
            this.Register(id, ipsalyzer => instance);
        }
        /// <summary>
        /// 指定されたIDに紐づくインスタンスサプライヤ(インスタンス生成方法)を登録します。
        /// </summary>
        /// <param name="TEAM_ID">インスタンスID</param>
        /// <param name="supplier">インスタンスサプライヤ</param>
        /// <param name="useCache">インスタンス生成後にインスタンスをキャッシュする場合true</param>
        public void Register(string id, Func<IpsalyzerApp, Object> supplier, bool useCache = false)
        {
            this.resolverDictionary[id] =
                Tuple.Create<Func<IpsalyzerApp, Object>, bool>(supplier, useCache);
            if (this.instanceCache.ContainsKey(id))
            {
                this.instanceCache.Remove(id);
            }
        }
        #endregion //end of Register methods

        #region Instance methods
        /// <summary>
        /// 指定された型・インスタンス名に紐づくインスタンスを取得し、返却します。
        /// </summary>
        /// <typeparam name="T">型名</typeparam>
        /// <param name="name">任意のインスタンス名</param>
        /// <param name="allowCache">キャッシュインスタンスを利用する場合はtrue</param>
        /// <returns>取得されたインスタンス</returns>
        public T Instance<T>(string name = "default", bool allowCache = false) where T : class
        {
            return Instance(typeof(T), name, allowCache) as T;
        }
        /// <summary>
        /// 指定された型・インスタンス名に紐づくインスタンスを取得し、返却します。
        /// </summary>
        /// <param name="type">型名</param>
        /// <param name="name">任意のインスタンス名</param>
        /// <param name="allowCache">キャッシュインスタンスを利用する場合はtrue</param>
        /// <returns>取得されたインスタンス</returns>
        public Object Instance(Type type, string name = "default", bool allowCache = false)
        {
            return Instance(this.GetInstanceId(type, name), allowCache);
        }
        /// <summary>
        /// 指定されたインスタンスIDのインスタンスを取得し、返却します。
        /// </summary>
        /// <param name="TEAM_ID">インスタンスID</param>
        /// <param name="allowCache">キャッシュインスタンスを利用する場合はtrue</param>
        /// <returns>取得されたインスタンス</returns>
        public Object Instance(string id, bool allowCache = false)
        {
            if (allowCache && this.instanceCache.ContainsKey(id))
            {
                return this.instanceCache[id];
            }
            Tuple<Func<IpsalyzerApp, Object>, bool> resolver;
            if (!this.resolverDictionary.TryGetValue(id, out resolver))
            {
                return null;
            }
            if (resolver == null)
            {
                return null;
            }
            Func<IpsalyzerApp, Object> supplier = resolver.Item1;
            bool useCache = resolver.Item2;
            if (supplier == null)
            {
                return null;
            }

            var result = supplier.Invoke(this);
            if (useCache)
            {
                this.instanceCache[id] = result;
                if (result is IDisposable)
                {
                    this.finalizeList.Add(result);
                }
            }
            return result;

        }
        #endregion //end of Instance methods

        #region GetInstanceId methods
        /// <summary>
        /// インスタンス型とインスタンス名よりインスタンスIDを生成し、返却します。
        /// </summary>
        /// <param name="type">インスタンス型</param>
        /// <param name="name">インスタンス名</param>
        /// <returns>インスタンスID</returns>
        public string GetInstanceId(Type type, string name = "default")
        {
            if (type == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "default";
            }
            return type.ToString() + "_" + name;
        }
        #endregion //end of GetInstanceId methods

        /// <summary>
        /// 指定されたPageオブジェクトを表示します。
        /// </summary>
        /// <param name="page">ページオブジェクト</param>
        /// <param name="animated">アニメーションをする場合はtrue</param>
        /// <returns>Taskオブジェクト</returns>
        public Task ShowNextPage(Page page, bool animated = false)
        {
            return Application.Current.MainPage.Navigation.PushAsync(page, animated);
        }
        /// <summary>
        /// 現在のページを閉じ、元の画面に戻ります。
        /// </summary>
        /// <param name="animated">アニメーションをする場合はtrue</param>
        /// <returns>Taskオブジェクト</returns>
        public Task CloseCurrentPage(bool animated = false)
        {
            return Application.Current.MainPage.Navigation.PopAsync(animated);
        }

        #endregion //end of public instance methods
    }
}
