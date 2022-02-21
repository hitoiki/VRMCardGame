using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class CoinViewingPermanentDeco : IPermanent, ICoinObservable
{
    IPermanent permanent;
    Subject<(Coin key, int changeValue, int result)> coinSubject = new Subject<(Coin key, int changeValue, int result)>();

    public CoinViewingPermanentDeco(IPermanent newPermanent)
    {
        permanent = newPermanent;
    }
    public IObservable<(Coin key, int changeValue, int result)> GetObservableCoin()
    {
        return coinSubject as IObservable<(Coin key, int changeValue, int result)>;
    }
    public ICard GetCard()
    {
        return permanent.GetCard();
    }
    public void SetCard(ICard newCard)
    {
        permanent.SetCard(newCard);
    }
    public IDeck OnDeck()
    {
        return permanent.OnDeck();
    }
    public (Coin coin, int result) ChangeCoin(Coin c, int n)
    {
        (Coin coin, int result) changeResult = permanent.ChangeCoin(c, n);
        coinSubject.OnNext((changeResult.coin, n, changeResult.result));
        return changeResult;
    }
    public bool MoveDeck(IDeck toDeck)
    {
        return permanent.MoveDeck(toDeck);
    }
    public Context GetContext()
    {
        return permanent.GetContext();
    }
    public EffectProjector GetEffectProjector()
    {
        return permanent.GetEffectProjector();
    }

    public void Dispose()
    {
        coinSubject.Dispose();
    }
}