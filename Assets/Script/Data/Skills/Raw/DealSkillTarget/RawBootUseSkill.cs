using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawBootUseSkill : IRawSkill
{
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            facade.skillQueue.Push(facade.skillTarget.GetSkillPack().UseProcess(), facade.skillTarget);
            return Observable.Empty<Unit>();
        });
    }

    public string Text()
    {
        return "使用する。";
    }

    public string SkillName()
    {
        return "BootUseSkill";
    }
}
