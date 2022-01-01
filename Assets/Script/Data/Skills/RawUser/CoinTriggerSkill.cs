using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

[System.Serializable]
public class CoinTriggerSkill : ICoinProcess
{
    [SerializeField] private Coin ReactiveCoin;
    [SerializeReference, SubclassSelector] private IRawSkill rawSkill;

    public IObservable<Unit> GetSkillProcess(CardFacade facade, Coin c, int n)
    {
        return Observable.Defer<Unit>(() =>
        {
            IObservable<Unit> skillObservable = Observable.Empty<Unit>();
            skillObservable = rawSkill.GetSkillProcess(facade);
            return skillObservable;
        });
    }


    public bool GetIsSkillable(CardFacade facade, Coin coin, int n)
    {

        return ReactiveCoin == coin;

    }

    public string Text()
    {
        return ReactiveCoin.name + "を受け取った時、" + rawSkill.Text();
    }

    public string SkillName()
    {
        return ReactiveCoin.name + "Triggered[" + rawSkill.SkillName() + "]";
    }

}
