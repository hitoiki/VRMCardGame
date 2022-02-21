using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

[System.Serializable]
public class CoinTriggerSkill : ISkillProcessCoin
{
    [SerializeField] private Coin ReactiveCoin;
    [SerializeReference, SubclassSelector] private IRawSkill rawSkill;

    public IObservable<Unit> GetSkillProcess(CardFacade facade, (Coin c, int n) value)
    {
        return Observable.Defer<Unit>(() =>
        {
            IObservable<Unit> skillObservable = Observable.Empty<Unit>();
            skillObservable = rawSkill.GetSkillProcess(facade);
            return skillObservable;
        });
    }


    public bool GetIsSkillable(CardFacade facade, (Coin c, int n) value)
    {

        return ReactiveCoin == value.c;

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
