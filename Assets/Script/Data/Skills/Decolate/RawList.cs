using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

public class RawList : IRawSkill
{
    //RawSkillを一つのリストに纏めるやつ

    [SerializeReference, SubclassSelector] List<IRawSkill> rawSkills;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
       {

           return Observable.Concat<Unit>(rawSkills.Select(x => { return x.GetSkillProcess(facade); }));
       });

    }
    public string Text()
    {
        return rawSkills.Aggregate("", (str, skill) => { return str + "\n" + skill.Text(); });
    }

    public string SkillName()
    {
        return rawSkills.Aggregate("", (str, skill) => { return str + "," + skill.SkillName(); }); ;
    }
}
