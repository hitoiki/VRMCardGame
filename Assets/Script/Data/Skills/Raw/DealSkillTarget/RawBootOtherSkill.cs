using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawBootOtherSkill : IRawSkill
{
    [SerializeField] private OtherSkillKind kind;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            facade.skillTarget.BootOtherSkill(kind, facade.skillQueue);
            return Observable.Empty<Unit>();
        });
    }

    public string Text()
    {
        return kind.ToBootingText();
    }

    public string SkillName()
    {
        return "Boot" + kind.KindName();
    }
}
