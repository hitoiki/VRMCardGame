using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OtherSkillKindExtention
{
    //OtherSkillKindの拡張メソッドを記述する
    public static string KindName(this OtherSkillKind kind)
    {
        if (kind == OtherSkillKind.Click) return "click";
        if (kind == OtherSkillKind.turnEnd) return "turnEnd";
        if (kind == OtherSkillKind.turnStart) return "turnStart";
        return "";
    }

    public static string ToCardText(this OtherSkillKind kind)
    {
        if (kind == OtherSkillKind.Click) return "クリックされた時";
        if (kind == OtherSkillKind.turnEnd) return "ターン終了時";
        if (kind == OtherSkillKind.turnStart) return "ターン開始時";
        return "";
    }

    public static string ToBootingText(this OtherSkillKind kind)
    {
        if (kind == OtherSkillKind.Click) return "クリックする。";
        if (kind == OtherSkillKind.turnEnd) return "ターン終了と知らせる。";
        if (kind == OtherSkillKind.turnStart) return "ターン開始と知らせる。";
        return "";
    }
}
