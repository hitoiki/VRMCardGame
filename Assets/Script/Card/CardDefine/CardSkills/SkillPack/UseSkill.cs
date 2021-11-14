using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UseSkill
{
    [SerializeReference, SubclassSelector] public IUseProcess useSkill;
    [SerializeReference, SubclassSelector] public ISkillEffect[] effect;
    public UseSkill(IUseProcess UseSkill, ISkillEffect[] Effect)
    {
        this.effect = Effect;
        this.useSkill = UseSkill;
    }

    public Skill GetSkill()
    {
        return new Skill(effect, useSkill.GetProcess()) + costPay;
    }
    //使用時のコスト支払い
    public static Skill costPay = new Skill(new ISkillEffect[0], (x) => { x.CostPay(); });
}
