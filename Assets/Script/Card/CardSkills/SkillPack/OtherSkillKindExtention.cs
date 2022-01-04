using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OtherSkillKindExtention
{
    //OtherSkillKindの拡張メソッドを記述する
    public static string KindName(this OtherSkillKind kind)
    {
        if (kind == OtherSkillKind.Click) return "click";
        if (kind == OtherSkillKind.TurnEnd) return "turnEnd";
        if (kind == OtherSkillKind.TurnStart) return "turnStart";
        if (kind == OtherSkillKind.OnPick) return "onPic";
        if (kind == OtherSkillKind.OnAction) return "onAction";
        return "";
    }

    public static string ToCardText(this OtherSkillKind kind)
    {
        if (kind == OtherSkillKind.Click) return "クリックされた時";
        if (kind == OtherSkillKind.TurnEnd) return "ターン終了時";
        if (kind == OtherSkillKind.TurnStart) return "ターン開始時";
        if (kind == OtherSkillKind.OnPick) return "取得時";
        if (kind == OtherSkillKind.OnAction) return "プレイヤーが行動するたび";
        return "";
    }

    public static string ToBootingText(this OtherSkillKind kind)
    {
        if (kind == OtherSkillKind.Click) return "クリックする。";
        if (kind == OtherSkillKind.TurnEnd) return "ターン終了と知らせる。";
        if (kind == OtherSkillKind.TurnStart) return "ターン開始と知らせる。";
        if (kind == OtherSkillKind.OnPick) return "取得時効果を発動する";
        if (kind == OtherSkillKind.OnAction) return "プレイヤーが行動したと知らせる";
        return "";
    }
}
