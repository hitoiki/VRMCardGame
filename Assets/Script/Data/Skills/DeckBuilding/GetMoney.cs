using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class GetMoney : IRawSkill
{
    [SerializeReference, SubclassSelector] private ISkillInt number;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            facade.instantMoney += number.SkillInt(facade);
            return Observable.Empty<Unit>();
        });
    }

    public string Text()
    {
        return "金を" + number.Text() + "加える。";
    }

    public string SkillName()
    {
        return "Getmoney(" + number.SkillName() + ")";
    }
}

