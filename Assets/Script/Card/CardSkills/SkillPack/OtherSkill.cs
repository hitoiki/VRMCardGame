using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

[System.Serializable]
public class OtherSkill : ISkillProcessKind
{
    //ターン終了時など、その他特定の状況で発動する
    //SkillTimingごとに違った対応をするのはSkillの意味の把握を困難にする
    //ので、IRawSkillベースで作る
    [SerializeReference, SubclassSelector] public IRawSkill rawSkill;
    [SerializeField] public OtherSkillKind timing;

    public OtherSkill(IRawSkill RawSkill, OtherSkillKind timing)
    {
        this.rawSkill = RawSkill;
    }

    public IObservable<Unit> GetSkillProcess(CardFacade facade, OtherSkillKind Timing)
    {
        if (timing == Timing) return rawSkill.GetSkillProcess(facade);
        else return Observable.Empty<Unit>();
    }

    public bool GetIsSkillable(CardFacade facade, OtherSkillKind Timing)
    {
        return timing == Timing;
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

public enum OtherSkillKind
{
    TurnEnd, TurnStart, Click, OnPick, OnAction
}
