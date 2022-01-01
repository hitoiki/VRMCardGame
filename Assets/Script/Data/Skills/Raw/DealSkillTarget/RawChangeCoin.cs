using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawChangeCoin : IRawSkill
{
    [SerializeField] private Coin c;
    [SerializeReference, SubclassSelector] private ISkillInt number;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            facade.skillTarget.ChangeCoin(c, number.SkillInt(facade));
            return Observable.Empty<Unit>();
        });
    }

    public string Text()
    {
        return c.name + "を" + number.Text() + "枚与える。";
    }

    public string SkillName()
    {
        return "changeCoin(" + c.name + "," + number.SkillName() + ")";
    }
}

