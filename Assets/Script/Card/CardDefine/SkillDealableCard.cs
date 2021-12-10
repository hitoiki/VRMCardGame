using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class SkillDealableCard
{
    // Skill上でICardを扱う際の諸々の処理に対応させたクラス
    // ICardの名前を変えたい
    private ICardPrintable printable;
    private ICard card;
    private IDeck onDeck;
    private SkillQueueObject skillQueue;
    public SkillDealableCard(ICardPrintable Printable, IDeck Deck, SkillQueueObject QueueObject)
    {
        printable = Printable;
        card = printable.GetCard();
        onDeck = Deck;
        skillQueue = QueueObject;
    }
    public Dictionary<Coin, int> GetCoin()
    {
        return card.GetCoin();
    }
    public CardData GetCard()
    {
        return card.GetCardData();
    }
    public DeckType GetDeckType()
    {
        return onDeck.GetDeckType();
    }
    public void SetCard(CardData Card)
    {
        card.SetCard(Card);
    }
    public void SetSecondSkillPack(SkillPack PackSet)
    {
        card.SetSecondSkillPack(PackSet);
    }
    public void AddSecondSkillPack(SkillPack PackSet)
    {
        card.AddSecondSkillPack(PackSet);
    }
    public void ChangeCoin(Coin c, int n)
    {
        card.ChangeCoin(c, n);
        skillQueue.Push(card.GetSkillPack().CoinSkill(c, n), this, null);
    }
    public EffectLocation GetEffectTarget()
    {
        return new EffectLocation(printable, null);
    }

    public void AddTarget(EffectLocation location)
    {
        location.AddTarget(printable);
    }

    public void MoveDeck(IDeck toDeck)
    {
        onDeck.Remove(card);
        toDeck.Add(card);
        skillQueue.Push(card.GetSkillPack().DrawSkill(onDeck, toDeck), this, null);
    }
}
