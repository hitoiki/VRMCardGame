using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillExtention
{
    //SkillProcessの拡張メソッドを記述する
    public static Skill GetSkill(this ISkillProcessUse useSkill)
    {
        return new Skill(useSkill.SkillName(), useSkill.GetSkillProcess, useSkill.GetIsSkillable);// * CostPaySkill.costPay;
    }
    public static Skill GetSkill<T>(this ISkillProcess<T> skillProcess, T t)
    {
        return new Skill(skillProcess.SkillName(), x => skillProcess.GetSkillProcess(x, t), x => skillProcess.GetIsSkillable(x, t));
    }

}
