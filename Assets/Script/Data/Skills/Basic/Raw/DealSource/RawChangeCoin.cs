using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawChangeCoin : IRawSkill
{
    [SerializeField] private Coin c;
    [SerializeField] private short amount = 0;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        facade.source.ChangeCoin(c, amount);
        return Observable.Empty<Unit>();
    }

    public string Text()
    {
        return c.name + "を" + amount.ToString() + "枚与える。";
    }

    public string SkillName()
    {
        return "changeCoin(" + c.coinName + "," + amount.ToString() + ")";
    }
}

