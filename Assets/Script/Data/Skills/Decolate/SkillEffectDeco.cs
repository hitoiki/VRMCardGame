using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class SkillEffectDeco : IRawSkill
{
    [SerializeReference, SubclassSelector] public IRawSkill rawSkill;
    [SerializeReference, SubclassSelector] ISkillEffect[] effect;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
       {
           IObservable<Unit> effectSubject = facade.skillsSubject.EffectLoad(effect, facade.source);
           return effectSubject.Concat(rawSkill.GetSkillProcess(facade));
       });
    }
    public string Text()
    {
        return rawSkill.Text();
    }

    public string SkillName()
    {
        return rawSkill.SkillName();
    }
}
