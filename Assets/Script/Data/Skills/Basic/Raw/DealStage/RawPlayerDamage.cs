using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawPlayerDamage : IRawSkill
{
    [SerializeField] int damage;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            facade.PlayerDamage(damage);
            return Observable.Empty<Unit>();
        });
    }

    public string Text()
    {
        return "プレイヤーに" + damage.ToString() + "ダメージ。";
    }

    public string SkillName()
    {
        return "PlayerDamage:" + damage.ToString();
    }
}