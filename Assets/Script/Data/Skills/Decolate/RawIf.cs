using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawIf : IRawSkill
{
    [SerializeReference, SubclassSelector] private IRawSkill rawSkill;
    [SerializeReference, SubclassSelector] private ISkillBool condition;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            IObservable<Unit> skillObservable = Observable.Empty<Unit>();
            if (condition.SkillBool(facade))
            {
                skillObservable = rawSkill.GetSkillProcess(facade);
            }
            return skillObservable;
        });
    }


    public string Text()
    {
        return condition.Text() + "ならば、" + rawSkill.Text();
    }

    public string SkillName()
    {
        return "if(" + condition.SkillName() + "){" + rawSkill.SkillName() + "}";
    }
}
