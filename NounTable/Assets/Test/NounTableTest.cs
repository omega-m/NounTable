using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nt;

/// <summary>NounTable�e�X�g�p</summary>
public class NounTableTest : MonoBehaviour{
    
    
    #region Unity���ʏ���
    void Start(){
        m_NounTable = GetComponent<NounTable>();

        //�Ђ炪�ȂɑΉ����閼���f�[�^�����邩�`�F�b�N�y�ю擾
        NounRecord rec;
        if (m_NounTable.TryGetRecord("�����", out rec)) {
            Debug.Log(rec.Hiragana + " " + rec.Original + "" + rec.Kind);
        }
        if (m_NounTable.TryGetRecord("��������", out rec)) {
            Debug.Log(rec.Hiragana + " " + rec.Original + "" + rec.Kind);
        }
        if (m_NounTable.TryGetRecord("�ӂ�����", out rec)) {
            Debug.Log(rec.Hiragana + " " + rec.Original + "" + rec.Kind);
        }
    }

    void Update(){
        
    }
    #endregion

    #region �����o
    private NounTable m_NounTable;
    #endregion

}
