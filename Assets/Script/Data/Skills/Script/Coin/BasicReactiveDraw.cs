using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
[System.Serializable]
public class BasicReactiveDraw : ICoinProcess
{
    [SerializeField] private Coin ReactiveCoin;
    [SerializeField] private int threshold = 0;
    [SerializeField] private StageDeck drawFrom;
    [SerializeField] private StageDeck drawTo;
    [SerializeField] private int drawAmount = 0;
    public void GetSkillProcess(CardFacade facade, Coin c, int n)
    {
        if (facade.sourceCoins.ContainsKey(ReactiveCoin) && facade.sourceCoins[ReactiveCoin] >= threshold)
        {
            facade.DeckDraw(drawFrom, drawTo, drawAmount);
            facade.CoinAdjustSource(ReactiveCoin, -threshold);
        }
    }


    public bool GetIsSkillable(CardFacade facade, Coin coin, int n)
    {
        return ReactiveCoin == coin && facade.sourceCoins.ContainsKey(ReactiveCoin) && facade.sourceCoins[ReactiveCoin] >= threshold;
    }

    public string Text()
    {
        return ReactiveCoin.name + "が" + threshold.ToString() + "枚を超えた時、" + StageDeckMethod.ToCardText(drawFrom) +
        "から" + StageDeckMethod.ToCardText(drawFrom) + "枚引く。\nその後、" +
        ReactiveCoin.name + "を" + threshold.ToString() + "枚このカードから取り除く。";
    }

    public string SkillName()
    {
        return "basicReactiveDraw";
    }
}