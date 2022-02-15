using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using System.Linq;

public class SkillDealingCard : IPermanent
{
    SkillQueue skillQueue;
    ICard card;
    IDeck deck;
    EffectProjector projector = new EffectProjector();

    public SkillDealingCard(ICard newCard, IDeck newDeck, SkillQueue queue)
    {
        skillQueue = queue;
        card = newCard;
        deck = newDeck;
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
    public void ChangeCoin(Coin c, int n)
    {

        if (card.GetCoin().ContainsKey(c)) card.GetCoin()[c] += n;
        //ないなら追加
        else card.GetCoin().Add(c, n);
        //負数なら削除
        if (card.GetCoin()[c] < 0) card.GetCoin().Remove(c);
        skillQueue.Push(card.GetSkillPack().CoinSkill(c, n), this);
    }

    public void MoveDeck(IDeck toDeck)
    {
        skillQueue.Push(card.GetSkillPack().DrawSkill(deck, toDeck), this);
        deck.Remove(this.card);
        toDeck.Add(this.card);
        //これDrawするはずのPermanent消えちゃいますね
    }

    public EffectProjector GetEffectProjector()
    {
        return projector;
    }
}
