using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class DataPermanent : IPermanent
{

    ICard card;
    IDeck deck;
    Context context;
    public DataPermanent(ICard newCard, IDeck newDeck, Context newContext)
    {
        card = newCard;
        deck = newDeck;
        context = newContext;
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
        return (c, card.GetCoin()[c]);
    }

    public IPermanent MoveDeck(IDeck toDeck)
    {
        if (deck.RemoveCheck(this.card) && toDeck.AddCheck(this.card))
        {
            deck.Remove(this.card);
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
        return null;
    }

    public void Dispose()
    {

    }
}
