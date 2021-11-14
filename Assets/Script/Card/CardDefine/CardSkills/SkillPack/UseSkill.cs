using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UseSkill
{
    [SerializeReference, SubclassSelector] public IUseProcess useSkill;
    [SerializeReference, SubclassSelector] public ISkillEffect[] effect;
    public UseSkill(IUseProcess UseSkill, ISkillEffect[] Effect, SkillCondition Condition)
    {
        this.effect = Effect;
        this.useSkill = UseSkill;
    }

    public Skill GetSkill()
    {
        return new Skill(effect, useSkill.GetProcess());
    }
}
