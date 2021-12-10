using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeck
{
    // Deckの疎結合
    //どんなDeckか確認
    DeckType GetDeckType();
    //代入
    void Substitution(List<ICard> c);
    //追加
    void Add(ICard c);
    //削除
    void Remove(ICard c);
    //指定したカードがあるなら取り出し、ないのならnullを返す
    List<ICard> Pick(List<ICard> cs);
    //指定したカードが存在するかを確認
    bool ExistCheck(ICard c);
    //ドロー処理
    List<ICard> Draw(int n);
    //山札の上をチェック
    List<ICard> DrawCheck(int i);
    //シャッフル
    void Shuffle();
}
