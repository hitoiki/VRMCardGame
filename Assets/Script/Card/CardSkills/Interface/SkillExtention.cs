using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillExtention
{
    //SkillProcessの拡張メソッドを記述する
    public static Skill GetSkill(this IUseProcess useSkill)
    {
        return new Skill(useSkill.SkillName(), useSkill.GetSkillProcess, useSkill.GetIsSkillable);// * CostPaySkill.costPay;
    }
    public static Skill GetSkill(this ICoinProcess coinSkill, Coin coin, int n)
    {
        return new Skill(coinSkill.SkillName(), x => coinSkill.GetSkillProcess(x, coin, n), x => coinSkill.GetIsSkillable(x, coin, n));
    }
    public static Skill GetSkill(this IDrawProcess drawSkill, IDeck from, IDeck to)
    {
        return new Skill(drawSkill.SkillName(), x => drawSkill.GetSkillProcess(x, from, to), x => drawSkill.GetIsSkillable(x, from, to));
    }

    public static Skill GetSkill(this IPickingProcess drawSkill, IPermanent permanent)
    {
        return new Skill(drawSkill.SkillName(), x => drawSkill.GetSkillProcess(x, permanent), x => drawSkill.GetIsSkillable(x, permanent));
    }
}
