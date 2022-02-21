using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using System.Linq;

public class SkillDealingPermanent : IPermanent
{
    SkillQueue skillQueue;
    ICard card;
    IDeck deck;
    Context context;
    EffectProjector projector = new EffectProjector();

    public SkillDealingPermanent(ICard newCard, IDeck newDeck, Context newContext, SkillQueue queue)
    {
        skillQueue = queue;
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
        skillQueue.Push(card.GetSkillPack().SkillProcess<(Coin, int)>((c, n)), this);
        return (c, card.GetCoin()[c]);
    }

    public bool MoveDeck(IDeck toDeck)
    {
        if (deck.RemoveCheck(this.card) && toDeck.AddCheck(this.card))
        {
            deck.Remove(this.card);
            toDeck.Add(this.card);
            return true;
        }
        Debug.Log("fail");
        return false;
    }
    public Context GetContext()
    {
        return context;
    }
    public EffectProjector GetEffectProjector()
    {
        return projector;
    }

    public void Dispose()
    {

    }
}
