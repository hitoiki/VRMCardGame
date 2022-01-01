using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class UseRawSkill : IUseProcess
{
    [SerializeReference, SubclassSelector] public IRawSkill skill;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            IObservable<Unit> skillObservable = skill.GetSkillProcess(facade);
            return skillObservable;
        });
    }
    public bool GetIsSkillable(CardFacade facade)
    {
        return true;
    }
    public string Text()
    {
        return "このカードは" + skill.Text();
    }

    public string SkillName()
    {
        return "Use" + skill.SkillName();
    }
}
