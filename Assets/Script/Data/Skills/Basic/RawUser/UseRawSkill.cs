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
        skill.GetSkillProcess(facade);
        return Observable.Empty<Unit>();
    }
    public bool GetIsSkillable(CardFacade facade)
    {
        return true;
    }
    public string Text()
    {
        return skill.Text();
    }

    public string SkillName()
    {
        return "Use" + skill.SkillName();
    }
}
