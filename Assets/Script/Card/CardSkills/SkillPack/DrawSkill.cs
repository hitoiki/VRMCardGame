using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DrawSkill
{
    [SerializeReference, SubclassSelector] public IDrawProcess drawSkill;
    [SerializeReference, SubclassSelector] public ISkillEffect[] effect;
    public DrawSkill(IDrawProcess DrawSkill, ISkillEffect[] Effect)
    {
        this.effect = Effect;
        this.drawSkill = DrawSkill;
    }

    public Skill GetSkill(IDeck from, IDeck to)
    {
        return new Skill(drawSkill.SkillName(), effect, x => drawSkill.GetSkillProcess(x, from, to), x => drawSkill.GetIsSkillable(x, from, to));
    }
}


