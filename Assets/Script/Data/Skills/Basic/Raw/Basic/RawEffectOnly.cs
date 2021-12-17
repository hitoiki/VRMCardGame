using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawEffectOnly : IRawSkill
{
    //何もしないSkill
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Empty<Unit>();
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
