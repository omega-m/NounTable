using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace nt {
    namespace inner {

        /// <summary>Mecab用辞書データから、独自の辞書データを作成します。</summary>
        public class NounDicGenerator : MonoBehaviour {


            #region Unity共通処理
            void Start() {
                const int CSV_KATAKANA_FIELD = 11;
                const int CSV_ORIGINAL_FIELD = 0;
                const int CSV_KIND_FIELD = 5;

                NounDic nounDic = new NounDic();

                //参照元meCab用辞書ファイルからデータを取得
                foreach (TextAsset dic in m_inDicAsset) {
                    CsvReadHelper csv = new CsvReadHelper(dic);
                    foreach (List<string> csvRcd in csv.Datas) {
                        try {
                            string hira = Util.KatakanaToHiragana(csvRcd[CSV_KATAKANA_FIELD]);
                            string origin = csvRcd[CSV_ORIGINAL_FIELD];
                            string kind = csvRcd[CSV_KIND_FIELD];

                            NounRecord nRcd = new NounRecord(hira, origin, kind);
                            nounDic.AddRecord(nRcd);
                        } catch (Exception) {
                            //none;
                        }
                    }
                }

                //書き出し
                nounDic.SaveTo(m_outFilePath);
                Debug.Log("NounDicGenerator.cs:名詞辞書データ[" + m_outFilePath + "]への書き出しに成功しました。");
            }
            #endregion


            #region インスペクタープロパティ
            [Tooltip(
                "参照元meCab用辞書データファイル\n" +
                "指定したファイルから、独自の辞書データを作成します。\n"
                )]
            [SerializeField] private List<TextAsset> m_inDicAsset = new List<TextAsset>();

            [Space(10)]
            [Tooltip("作成した独自辞書ファイル出力パス\n" +
                "Assets/Resources/ファイルがルートとなります。")]
            [SerializeField] private string m_outFilePath = "NounTable/noun.csv";
            #endregion
        }
    }
}
