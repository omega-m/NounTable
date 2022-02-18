using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nt;

/// <summary>NounTableテスト用</summary>
public class NounTableTest : MonoBehaviour{
    
    
    #region Unity共通処理
    void Start(){
        m_NounTable = GetComponent<NounTable>();

        //ひらがなに対応する名詞データがあるかチェック及び取得
        NounRecord rec;
        if (m_NounTable.TryGetRecord("さんま", out rec)) {
            Debug.Log(rec.Hiragana + " " + rec.Original + "" + rec.Kind);
        }
        if (m_NounTable.TryGetRecord("かいだん", out rec)) {
            Debug.Log(rec.Hiragana + " " + rec.Original + "" + rec.Kind);
        }
        if (m_NounTable.TryGetRecord("ふあうち", out rec)) {
            Debug.Log(rec.Hiragana + " " + rec.Original + "" + rec.Kind);
        }
    }

    void Update(){
        
    }
    #endregion

    #region メンバ
    private NounTable m_NounTable;
    #endregion

}
