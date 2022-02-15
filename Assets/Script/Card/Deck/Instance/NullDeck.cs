using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class NullDeck : IStagingDeck
{
    //空っぽのデッキ
    public void Init(DeckType type)
    {

    }

    public DeckType GetDeckType()
    {
        return DeckType.nullDeck;
    }
    //代入
    public void Substitution(List<ICard> c) { }
    //追加
    public bool Add(ICard c) { return false; }
    //削除
    public bool Remove(ICard c) { return false; }
    //指定したカードが存在するかを確認
    public bool ExistCheck(ICard c) { return false; }
    //Deckにカードが存在するか確認
    public bool Any() { return false; }
    //Deck枚数の確認
    public int Count() { return 0; }
    //ドロー処理
    public List<ICard> Draw(int n) { return null; }
    //山札の上をチェック
    public List<ICard> DrawCheck(int n) { return null; }
    //シャッフル
    public void Shuffle() { }

    public IEnumerator<IPermanent> GetEnumerator()
    {
        return null;
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return null;
    }
    public IObservable<CollectionReplaceEvent<IPermanent>> ReplaceEvent()
    {
        return Observable.Never<CollectionReplaceEvent<IPermanent>>();
    }
    public IObservable<CollectionAddEvent<IPermanent>> AddEvent()
    {
        return Observable.Never<CollectionAddEvent<IPermanent>>();
    }
    public IObservable<CollectionRemoveEvent<IPermanent>> RemoveEvent()
    {
        return Observable.Never<CollectionRemoveEvent<IPermanent>>();
    }
}
