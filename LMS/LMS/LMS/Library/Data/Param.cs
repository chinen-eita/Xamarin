using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Library.Data
{
    /// <summary>
    /// パラメータクラスの定義です。
    /// </summary>
    public class Param
    {
        /// <summary>
        /// パラメータ名・パラメータ値ディクショナリ
        /// </summary>
        private Dictionary<string, object> keyValueDictionary = new Dictionary<string, object>();

        #region public static methods

        #region Of methods
        /// <summary>
        /// パラメータ名とパラメータ値を指定してParamオブジェクトを生成し、返却します。
        /// </summary>
        /// <param name="name">パラメータ名</param>
        /// <param name="value">パラメータ値</param>
        /// <returns>Paramオブジェクト</returns>
        public static Param Of(string name, object value)
        {
            var result = new Param();
            result.Add(name, value);
            return result;
        }
        /// <summary>
        /// パラメータ名・パラメータ値ディクショナリを指定してParamオブジェクトを生成し、返却します。
        /// </summary>
        /// <param name="keyValueDictionary">パラメータ名・パラメータ値ディクショナリ</param>
        /// <returns>Paramオブジェクト</returns>
        public static Param Of(Dictionary<string, object> keyValueDictionary)
        {
            var result = new Param();
            result.Add(keyValueDictionary);
            return result;
        }
        #endregion //end of Of methods

        #endregion //end of public static methods

        #region public instance methods

        #region Add methods
        /// <summary>
        /// パラメータ名・パラメータ値ディクショナリをParamオブジェクトへ追加し、Paramオブジェクトを返却します。
        /// </summary>
        /// <param name="keyValueDictionary">パラメータ名・パラメータ値ディクショナリ</param>
        /// <returns>Paramオブジェクト</returns>
        public Param Add(Dictionary<string, object> keyValueDictionary)
        {
            if (keyValueDictionary == null)
            {
                return this;
            }
            foreach (var keyValue in keyValueDictionary)
            {
                this.Add(keyValue.Key, keyValue.Value);
            }
            return this;
        }
        /// <summary>
        /// パラメータ名とパラメータ値を追加して、Paramオブジェクトを返却します。
        /// </summary>
        /// <param name="name">パラメータ名</param>
        /// <param name="value">パラメータ値</param>
        /// <returns></returns>
        public Param Add(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
            {
                return this;
            }
            if (value == null && this.keyValueDictionary.ContainsKey(name))
            {
                this.keyValueDictionary.Remove(name);
            }
            if (value != null)
            {
                this.keyValueDictionary[name] = value;
            }
            return this;
        }
        #endregion //end of Add methods

        #region Val methods
        /// <summary>
        /// パラメータ名を指定してパラメータ値を取得します。存在しない場合はデフォルト値を返却します。
        /// </summary>
        /// <typeparam name="T">パラメータ型</typeparam>
        /// <param name="name">パラメータ名</param>
        /// <param name="defaultValue">デフォルト値</param>
        /// <returns>パラメータ値</returns>
        public T Val<T>(string name, T defaultValue = null) where T : class
        {
            var result = this.Val(name) as T;
            return result != null ? result : defaultValue;
        }
        /// <summary>
        /// パラメータ名を指定してパラメータ値を取得します。存在しない場合はデフォルト値を返却します。
        /// </summary>
        /// <param name="name">パラメータ名</param>
        /// <param name="defaultValue">デフォルト値</param>
        /// <returns></returns>
        public object Val(string name, object defaultValue = null)
        {
            object result;
            return this.keyValueDictionary.TryGetValue(name, out result) ? result : defaultValue;
        }
        #endregion //end of Val methods

        #endregion //end of public instance methods
    }
}
