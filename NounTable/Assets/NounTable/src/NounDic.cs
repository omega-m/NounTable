using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

namespace nt {

    /// <summary>名詞辞書のレコードクラス</summary>
    public class NounRecord {

        #region 生成
        /// <summary>名詞辞書のレコードクラス</summary>
        /// <param name="aHiragana">ひらがな文字列</param>
        /// <param name="aOriginal">元の文字列(漢字等)</param>
        /// <param name="aKind">種類(固有名詞等)</param>
        public NounRecord(string aHiragana = "", string aOriginal = "", string aKind = "") {
            Hiragana = aHiragana;
            Original = aOriginal;
            Kind = aKind;
        }

        /// <summary>名詞辞書のレコードクラス</summary>
        /// <param name="aSrc">コピー元</param>
        public NounRecord(in NounRecord aSrc) {
            Hiragana = aSrc.Hiragana;
            Original = aSrc.Original;
            Kind = aSrc.Original;
        }
        #endregion


        #region プロパティ
        /// <summary>ひらがな文字列</summary>
        public string Hiragana = "";
        /// <summary>元の文字列(漢字等)</summary>
        public string Original = "";
        /// <summary>種類(固有名詞等)</summary>
        public string Kind = "";
        #endregion
    }




    namespace inner {

        /// <summary>名詞データを格納した辞書クラス</summary>
        /// <example><code>
        /// 
        /// 
        /// //名詞データをテキストアセットから読み込み
        /// TextAsset textAsset = Resources.Load("NounTable/noun") as TextAsset;
        /// NounDic nounDic = new NounDic(textAsset);
        /// 
        /// 
        /// //ひらがなに対応する名詞データがあるかチェック及び取得
        /// NounRecord rec;
        /// if (nounDic.TryGetRecord("さんま", out rec)) {
        ///     Debug.Log(rec.Hiragana + " " + rec.Original + "" + rec.Kind);
        /// }
        /// if (nounDic.TryGetRecord("かいだん", out rec)) {
        ///     Debug.Log(rec.Hiragana + " " + rec.Original + "" + rec.Kind);
        /// }
        /// if (nounDic.TryGetRecord("ふあうち", out rec)) {
        ///     Debug.Log(rec.Hiragana + " " + rec.Original + "" + rec.Kind);
        /// }
        /// </code></example> 
        public class NounDic {


            #region 生成
            /// <summary>
            /// <para>名詞データを格納した辞書クラス</para>
            /// <para>空辞書として作成</para>
            /// </summary>
            /// <param name="hiraganaMinLength">ひらがな文字列の最低文字数</param>
            /// <param name="hiraganaMaxLength">ひらがな文字列の最大文字数</param>
            public NounDic(int hiraganaMinLength = 2, int hiraganaMaxLength = 10) {
                HiraganaMinLength = hiraganaMinLength;
                HiraganaMaxLength = hiraganaMaxLength;
            }

            /// <summary>名詞データを格納した辞書クラス</summary>
            /// <param name="aCsvAsset">名詞データ情報が入ったテキストアセット(.csv)</param>
            /// <param name="hiraganaMinLength">ひらがな文字列の最低文字数</param>
            /// <param name="hiraganaMaxLength">ひらがな文字列の最大文字数</param>
            public NounDic(TextAsset aCsvAsset, int hiraganaMinLength = 2, int hiraganaMaxLength = 10) {
                HiraganaMinLength = hiraganaMinLength;
                HiraganaMaxLength = hiraganaMaxLength;
                
                const int CSV_HIRAGANA_FIELD    = 0;
                const int CSV_ORIGINAL_FIELD    = 1;
                const int CSV_KIND_FIELD        = 2;

                CsvReadHelper csv = new CsvReadHelper(aCsvAsset);
                foreach (List<string> csvRcd in csv.Datas) {
                    string hira     = csvRcd[CSV_HIRAGANA_FIELD];
                    string origin   = csvRcd[CSV_ORIGINAL_FIELD];
                    string kind     = csvRcd[CSV_KIND_FIELD];

                    NounRecord nRcd = new NounRecord(hira, origin, kind);
                    AddRecord(nRcd);
                }
            }
            #endregion


            #region プロパティ
            private int m_hiraganaMinLength = 2;
            /// <summary>ひらがな文字列の最低文字数</summary>
            public int HiraganaMinLength {
                get { return m_hiraganaMinLength; } 
                set { 
                    if(value > m_hiraganaMinLength) {
                        Dictionary<string, NounRecord> tmp = new Dictionary<string, NounRecord>(m_datas);
                        foreach (string key in tmp.Keys) {
                            if(key.Length < value) {
                                m_datas.Remove(key);
                            }
                        }
                    }
                    m_hiraganaMinLength = value;
                }
            }

            private int m_hiraganaMaxLength = 10;
            /// <summary>ひらがな文字列の最大文字数</summary>
            public int HiraganaMaxLength {
                get { return m_hiraganaMaxLength; }
                set {
                    if (value < m_hiraganaMaxLength) {
                        Dictionary<string, NounRecord> tmp = new Dictionary<string, NounRecord>(m_datas);
                        foreach (string key in tmp.Keys) {
                            if (key.Length < value) {
                                m_datas.Remove(key);
                            }
                        }
                    }
                    m_hiraganaMaxLength = value;
                }
            }
            #endregion


            #region メソッド
            /// <summary>
            /// <para>名詞データを新たに追加します。</para>
            /// <para>既に同じひらがな文字列に対してデータがある場合、データを上書きします。</para>
            /// <para>ひらがなの文字数が[HiraganaMinLength]以下の場合は登録しません</para>
            /// </summary>
            /// <param name="aRecord">名詞データ</param>
            public void AddRecord(NounRecord aRecord) {
                if (aRecord.Hiragana.Length < HiraganaMinLength || HiraganaMaxLength < aRecord.Hiragana.Length) { return; }
                if (m_datas.ContainsKey(aRecord.Hiragana)) {
                    m_datas[aRecord.Hiragana] = aRecord;
                } else {
                    m_datas.Add(aRecord.Hiragana, aRecord);
                }
            }

            /// <summary>ひらがなに対応する名詞データがあるかチェック及び取得</summary>
            /// <param name="aHiragana">ひらがな</param>
            /// <param aRecord="aRecord">(レコードが存在する場合)名詞データ</param>
            /// <returns>true:レコードが存在する</returns>
            public bool TryGetRecord(string aHiragana, out NounRecord aRecord) {
                return m_datas.TryGetValue(aHiragana, out aRecord);
            }

            /// <summary>現在の辞書データをファイルに出力します</summary>
            public void SaveTo(string aExportPath) {
                string fullPath = System.IO.Directory.GetCurrentDirectory() + "\\Assets\\Resources\\" + aExportPath;
                StreamWriter file = new StreamWriter(@fullPath, false, Encoding.UTF8);
                foreach(var data in m_datas) {
                    file.WriteLine(string.Format("{0},{1},{2}", data.Value.Hiragana, data.Value.Original, data.Value.Kind));
                }
                file.Close();
            }
            #endregion


            #region メンバ
            private SortedDictionary<string, NounRecord> m_datas = new SortedDictionary<string, NounRecord>();
            #endregion
        }
    }
}