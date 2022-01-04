using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class SkillDealableCard
{
    // Skill上でICardを扱う際の諸々の処理に対応させたクラス
    private ICard card;
    public IDeck onDeck { get; private set; }
    private SkillQueueObject skillQueue;
    public ICardPrintable effectPrint { private get; set; }
    public SkillDealableCard(ICard Card, IDeck Deck, SkillQueueObject QueueObject)
    {
        card = Card;
        onDeck = Deck;
        skillQueue = QueueObject;
    }

    //カード操作
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
    public void MoveDeck(IDeck toDeck)
    {
        skillQueue.Push(card.GetSkillPack().DrawSkill(onDeck, toDeck), this);
        onDeck.Remove(card);
        toDeck.Add(card);
        onDeck = toDeck;
    }

    public void BootOtherSkill(OtherSkillKind kind)
    {
        skillQueue.Push(card.GetSkillPack().OtherSkill(kind), this);
    }

    //Effect処理用
    public IObservable<Unit> EffectBoot(ISkillEffect effect)
    {
        return effect.Effect(new EffectLocation(effectPrint));
    }
}
