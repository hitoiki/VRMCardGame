using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

[System.Serializable]
public class BasicReactiveDraw : ICoinProcess
{
    [SerializeField] private Coin ReactiveCoin;
    [SerializeField] private int threshold = 0;
    [SerializeField] private DeckType drawFrom;
    [SerializeField] private DeckType drawTo;
    [SerializeField] private int drawAmount = 0;
    public IObservable<Unit> GetSkillProcess(CardFacade facade, Coin c, int n)
    {
        if (facade.source.GetCoin().ContainsKey(ReactiveCoin) && facade.source.GetCoin()[ReactiveCoin] >= threshold)
        {
            facade.DeckDraw(drawFrom, drawTo, drawAmount);
            facade.source.ChangeCoin(ReactiveCoin, -threshold);
        }
        return Observable.Empty<Unit>();
    }


    public bool GetIsSkillable(CardFacade facade, Coin coin, int n)
    {
        return ReactiveCoin == coin && facade.source.GetCoin().ContainsKey(ReactiveCoin) && facade.source.GetCoin()[ReactiveCoin] >= threshold;
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