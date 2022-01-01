using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class RawSkillEffect : IRawSkill
{
    [SerializeReference, SubclassSelector] ISkillEffect[] effect;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
       {
           return facade.skillsSubject.EffectLoad(effect, facade.skillTarget);
       });
    }
    public string Text()
    {
        return "";
    }

    public string SkillName()
    {
        return "";
    }
}
