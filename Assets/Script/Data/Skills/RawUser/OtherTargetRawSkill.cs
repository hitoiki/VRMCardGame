using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class OtherTargetRawSkill : IRawSkill
{
    [SerializeReference, SubclassSelector] ISkillCard newTarget;
    [SerializeReference, SubclassSelector] IRawSkill rawSkill;

    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            return rawSkill.GetSkillProcess(facade.NewFacade(newTarget.SkillCard(facade)));
        });

    }
    public string Text()
    {
        return newTarget.Text() + "は" + rawSkill.Text();
    }

    public string SkillName()
    {
        return newTarget.SkillName() + ":" + rawSkill.SkillName();
    }
}
