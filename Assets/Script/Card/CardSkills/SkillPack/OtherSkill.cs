using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OtherSkill
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

    public Skill GetSkill(OtherSkillKind Timing)
    {
        if (timing == Timing) return new Skill(rawSkill.SkillName(), x => rawSkill.GetSkillProcess(x), x => true);
        else return null;
    }
}

public enum OtherSkillKind
{
    TurnEnd, TurnStart, Click, OnPick, OnAction
}
