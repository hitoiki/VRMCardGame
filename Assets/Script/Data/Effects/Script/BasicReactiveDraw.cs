﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
[CreateAssetMenu(fileName = "Data", menuName = "CardText/BasicReactiveDraw")]
public class BasicReactiveDraw : CoinText
{
    [SerializeField] private Coin ReactiveCoin;
    [SerializeField] private short threshold = 0;
    [SerializeField] private StageDeck drawFrom;
    [SerializeField] private StageDeck drawTo;
    [SerializeField] private short drawAmount = 0;
    public override void Effect(CardDealer dealer, Card target, Coin c, short n)
    {
        if (target.coins.ContainsKey(ReactiveCoin) && target.coins[ReactiveCoin] >= threshold)
        {
            dealer.DeckDraw(drawFrom, drawTo, drawAmount);
            target.RemoveCoin(dealer, ReactiveCoin, threshold);
        }
    }
    public override string Text()
    {
        return ReactiveCoin.coinName + " " + threshold.ToString() + ":カードを"
        + StageDeckMethod.ToCardText(drawFrom) + "から" + StageDeckMethod.ToCardText(drawTo)
        + "へ" + drawAmount.ToString() + "枚引く。";
    }
}