using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawPlayerDamage : IRawSkill
{
    [SerializeReference, SubclassSelector] private ISkillInt number;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            facade.PlayerDamage(number.SkillInt(facade));
            return Observable.Empty<Unit>();
        });
    }

    public string Text()
    {
        return "プレイヤーに" + number.Text() + "ダメージ。";
    }

    public string SkillName()
    {
        return "PlayerDamage:" + number.SkillName();
    }
}
