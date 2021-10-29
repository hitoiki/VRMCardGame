using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableDrawSkill : ScriptableObject, IDrawSkill
{

    protected abstract void Skill(CardFacade dealer, StageDeck from, StageDeck to);

    public SkillProcess DrawSkill(StageDeck from, StageDeck to)
    {
        return new SkillProcess(
        (CardFacade dealer) => { Skill(dealer, from, to); }
        );
    }
    public abstract string Text();
}

