using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

[System.Serializable]
public class CoinTriggerText : ICoinProcess
{
    [SerializeField] private Coin ReactiveCoin;
    [SerializeField] private int threshold = 0;
    [SerializeReference, SubclassSelector] private IUseProcess useText;

    public IObservable<Unit> GetSkillProcess(CardFacade facade, Coin c, int n)
    {
        if (facade.source.GetCoin()[ReactiveCoin] >= threshold)
        {
            useText.GetSkillProcess(facade);
            facade.source.ChangeCoin(c, -threshold);
        }
        return Observable.Empty<Unit>();
    }


    public bool GetIsSkillable(CardFacade facade, Coin coin, int n)
    {

        return ReactiveCoin == coin && facade.source.GetCoin().ContainsKey(ReactiveCoin) && facade.source.GetCoin()[ReactiveCoin] >= threshold;

    }

    public string Text()
    {
        return ReactiveCoin.name + "が" + threshold.ToString() + "以上になった時、それを" + threshold.ToString() + "消費して" + useText.Text();
    }

    public string SkillName()
    {
        return ReactiveCoin.coinName + "Triggered[" + useText.SkillName() + "]";
    }

}
