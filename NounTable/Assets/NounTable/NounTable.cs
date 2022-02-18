using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nt.inner;


namespace nt {

    /// <summary>ひらがなから名詞データを取得する為のクラス</summary>
    public class NounTable : MonoBehaviour {


        #region メソッド
        /// <summary>ひらがなに対応する名詞データがあるかチェック及び取得</summary>
        /// <param name="aHiragana">ひらがな</param>
        /// <param aRecord="aRecord">(レコードが存在する場合)名詞データ</param>
        /// <returns>true:レコードが存在する</returns>
        public bool TryGetRecord(string aHiragana, out NounRecord aRecord) {
            return m_dic.TryGetRecord(aHiragana, out aRecord);
        }
        #endregion


        #region Unity共通処理
        void Awake() {
            TextAsset textAsset = Resources.Load("NounTable/noun") as TextAsset;
            m_dic = new NounDic(textAsset);
        }
        #endregion


        #region メンバ
        private NounDic m_dic;
        #endregion


        #region 定数
        static private readonly string DIC_FILE_PATH = "NounTable/noun.csv";
        #endregion
    }
}