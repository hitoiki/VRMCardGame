using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawIf : IRawSkill
{
    [SerializeReference, SubclassSelector] private IRawSkill rawSkill;
    [SerializeReference, SubclassSelector] private IRawSkill elseRawSkill;
    [SerializeReference, SubclassSelector] private ISkillFacadeBool condition;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            IObservable<Unit> skillObservable = Observable.Empty<Unit>();
            if (condition.SkillBool(facade))
            {
                skillObservable = rawSkill.GetSkillProcess(facade);
            }
            else if (elseRawSkill != null) skillObservable = elseRawSkill.GetSkillProcess(facade);
            return skillObservable;
        });
    }


    public string Text()
    {
        if (elseRawSkill != null) return condition.Text() + "ならば、" + rawSkill.Text() + "そうでないなら、" + elseRawSkill.Text();
        return condition.Text() + "ならば、" + rawSkill.Text();
    }

    public string SkillName()
    {
        return "if(" + condition.SkillName() + "){" + rawSkill.SkillName() + "}";
    }
}
