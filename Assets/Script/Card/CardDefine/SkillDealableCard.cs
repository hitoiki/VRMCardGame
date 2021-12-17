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
    public IDeck onDeck { get; private set; }
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
    public CardData GetCardData()
    {
        return card.GetCardData();
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
        skillQueue.Push(card.GetSkillPack().CoinSkill(c, n), this);
    }
    public IObservable<Unit> EffectBoot(ISkillEffect effect)
    {
        return effect.Effect(new EffectLocation(printable));
    }

    public void MoveDeck(IDeck toDeck)
    {
        skillQueue.Push(card.GetSkillPack().DrawSkill(onDeck, toDeck), this);
        onDeck.Remove(card);
        toDeck.Add(card);
        onDeck = toDeck;
    }
}
