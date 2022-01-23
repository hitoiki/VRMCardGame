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
    bool Add(ICard c);
    //削除
    bool Remove(ICard c);
    //指定したカードがあるなら取り出し、ないのならnullを返す
    ICard Pick(ICard cs);
    //指定したカードが存在するかを確認
    bool ExistCheck(ICard c);
    //Deckにカードが存在するか確認
    bool Any();
    //ドロー処理
    List<ICard> Draw(int n);
    //山札の上をチェック
    List<ICard> DrawCheck(int n);
    //シャッフル
    void Shuffle();
}
