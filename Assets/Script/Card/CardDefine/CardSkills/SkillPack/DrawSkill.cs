using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DrawSkill
{
    [SerializeReference, SubclassSelector] public IDrawProcess drawSkill;
    [SerializeReference, SubclassSelector] public ISkillEffect[] effect;
    public DrawSkill(IDrawProcess DrawSkill, ISkillEffect[] Effect, SkillCondition Condition)
    {
        this.effect = Effect;
        this.drawSkill = DrawSkill;
    }

    public Skill GetSkill(StageDeck from, StageDeck to)
    {
        return new Skill(effect, drawSkill.GetProcess(from, to));
    }
}
