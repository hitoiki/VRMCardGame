﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
[System.Serializable]
public class BasicReactiveDraw : ICoinSkill
{
    [SerializeField] private Coin ReactiveCoin;
    [SerializeField] private int threshold = 0;
    [SerializeField] private StageDeck drawFrom;
    [SerializeField] private StageDeck drawTo;
    [SerializeField] private int drawAmount = 0;
    private void Skill(CardFacade facade, Coin c, int n)
    {
        if (facade.sourceCoins.ContainsKey(ReactiveCoin) && facade.sourceCoins[ReactiveCoin] >= threshold)
        {
            facade.DeckDraw(drawFrom, drawTo, drawAmount);
            facade.CoinToSource(ReactiveCoin, -threshold);
        }
    }
    public SkillProcess CoinSkill(Coin coin, int n)
    {
        return new SkillProcess(
        (CardFacade dealer) => { Skill(dealer, coin, n); }
        );
    }

    public string Text()
    {
        return ReactiveCoin.name + "が" + threshold.ToString() + "枚を超えた時、" + StageDeckMethod.ToCardText(drawFrom) +
        "から" + StageDeckMethod.ToCardText(drawFrom) + "枚引く。\nその後、" +
        ReactiveCoin.name + "を" + threshold.ToString() + "枚このカードから取り除く。";
    }
}