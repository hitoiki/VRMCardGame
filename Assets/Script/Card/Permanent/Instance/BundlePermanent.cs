using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class BundlePermanent : IPermanent, ICoinObservable
{
    SkillQueue skillQueue;
    ICard card;
    IDeck deck;
    Context context;
    EffectProjector projector = new EffectProjector();
    ReactiveProperty<int> _quantity = new ReactiveProperty<int>();
    public IReadOnlyReactiveProperty<int> quantity => _quantity as IReactiveProperty<int>;
    Subject<(Coin key, int changeValue, int result)> coinSubject = new Subject<(Coin key, int changeValue, int result)>();

    public BundlePermanent(ICard newCard, IDeck newDeck, Context newContext, SkillQueue queue, int newQuantity)
    {
        skillQueue = queue;
        card = newCard;
        deck = newDeck;
        context = newContext;
        _quantity.Value = newQuantity;
    }
    public ICard GetCard()
    {
        return card;
    }
    public void SetCard(ICard newCard)
    {
        card = newCard;
    }
    public IDeck OnDeck()
    {
        return deck;
    }
    public (Coin coin, int result) ChangeCoin(Coin c, int n)
    {
        if (card.GetCoin().ContainsKey(c)) card.GetCoin()[c] += n;
        //ないなら追加
        else card.GetCoin().Add(c, n);
        //負数なら削除
        if (card.GetCoin()[c] < 0) card.GetCoin().Remove(c);
        skillQueue.Push(card.GetSkillPack().ArgumentProcess<(Coin, int)>((c, n)), this);
        coinSubject.OnNext((c, n, card.GetCoin()[c]));
        return (c, card.GetCoin()[c]);
    }

    public IPermanent MoveDeck(IDeck toDeck)
    {
        if (deck.RemoveCheck(this.card) && toDeck.AddCheck(this.card))
        {
            _quantity.Value -= 1;
            if (_quantity.Value <= 0) deck.Remove(this.card);
            return toDeck.Add(this.card);
        }
        Debug.Log("fail");
        return null;
    }
    public bool MoveCheck(IDeck toDeck)
    {
        return deck.RemoveCheck(this.card) && toDeck.AddCheck(this.card);
    }
    public Context GetContext()
    {
        return context;
    }
    public EffectProjector GetEffectProjector()
    {
        return projector;
    }
    public IObservable<(Coin key, int changeValue, int result)> GetObservableCoin()
    {
        return coinSubject as IObservable<(Coin key, int changeValue, int result)>;
    }

    public void Dispose()
    {
        _quantity.Dispose();
        coinSubject.Dispose();
    }
}