using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using System;

public class Skill
{
    //識別用のタグ
    public string name;
    //実際に起こす処理
    public Func<CardFacade, IObservable<Unit>> process;
    //使用可能かの判定処理
    public Func<CardFacade, bool> isSkillable;

    public Skill(string Name, Func<CardFacade, IObservable<Unit>> Process, Func<CardFacade, bool> IsSkillable)
    {
        this.name = Name;
        this.process = Process;
        this.isSkillable = IsSkillable;
    }

    public static Skill operator *(Skill x, Skill y)
    {
        return new Skill(x.name + y.name, facade => { return x.process(facade).Concat(y.process(facade)); }, facade => { return x.isSkillable(facade) && y.isSkillable(facade); });
    }
}
