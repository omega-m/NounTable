# Name NounTable
ひらがなから対応する名詞があるかチェックし、取得出来るテーブル

# Installation
以下のパッケージを、Unityのプロジェクトにインポートしてください。  
[NounTable v0.1.0](https://github.com/omega-m/NounTable/releases/tag/NounTable_v_0_1_0)
  
# Usage
以下のスクリプトを、管理クラスのコンポーネントとして割り当ててください。  
Assets/NounTable/NounTable.cs  

NounTableを使用したいインスタンスに、以下のようなコードを加えてください。
    
    namespace nt;
    
    NounTable table = GetComponent&lt;NounTable&gt;();
    
    //ひらがなに対応する名詞データがあるかチェック及び取得
    NounRecord rec;
    if (nounDic.TryGetRecord("さんま", out rec)) {
        Debug.Log(rec.Hiragana + " " + rec.Original + "" + rec.Kind);
    }
    if (nounDic.TryGetRecord("かいだん", out rec)) {
        Debug.Log(rec.Hiragana + " " + rec.Original + "" + rec.Kind);
    }
    if (nounDic.TryGetRecord("ふあうち", out rec)) {
        Debug.Log(rec.Hiragana + " " + rec.Original + "" + rec.Kind);
    }

# Note
NounTableでは、MeCabの辞書データをお借りしています。(https://taku910.github.io/mecab/)

# Author
omega
