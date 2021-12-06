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

    public Skill GetSkill(StageDeck from, StageDeck to, DeckMove moveMode)
    {
        return new Skill(drawSkill.SkillName(), effect, x => drawSkill.GetSkillProcess(x, from, to, moveMode), x => drawSkill.GetIsSkillable(x, from, to, moveMode));
    }
}


