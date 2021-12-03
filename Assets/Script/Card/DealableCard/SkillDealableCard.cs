using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class SkillDealableCard
{
    // Skill上でIDealableを扱う際の諸々の処理に対応させたクラス
    // IDealableの名前を変えたい
    private ICardPrintable printable;
    private IDealableCard dealable;
    public StageDeck deck { get; }
    private SkillQueueObject skillQueue;
    public SkillDealableCard(ICardPrintable Printable, StageDeck Deck, SkillQueueObject QueueObject)
    {
        printable = Printable;
        dealable = printable.GetDealableCard();
        deck = Deck;
        skillQueue = QueueObject;
    }
    public Dictionary<Coin, int> GetCoin()
    {
        return dealable.GetCoin();
    }
    public Card GetCard()
    {
        return dealable.GetCard();
    }
    public void SetCard(Card Card)
    {
        dealable.SetCard(Card);
    }
    public void SetSecondSkillPack(SkillPack PackSet)
    {
        dealable.SetSecondSkillPack(PackSet);
    }
    public void AddSecondSkillPack(SkillPack PackSet)
    {
        dealable.AddSecondSkillPack(PackSet);
    }
    public void ChangeCoin(Coin c, int n)
    {
        dealable.ChangeCoin(c, n);
        skillQueue.Push(dealable.GetSkillPack().CoinSkill(c, n), this, null);
    }
    public EffectLocation GetEffectTarget()
    {
        return new EffectLocation(printable, null);
    }

    public void AddTarget(EffectLocation location)
    {
        location.AddTarget(printable);
    }
}
