using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableSelectSkill : ScriptableObject, ISelectSkill
{
    protected abstract void Skill(CardFacade dealer, List<Card> targets);
    public abstract (StageDeck, sbyte) SelectCard(GamePlayData data);

    public SkillProcess SelectSkill(List<Card> targets)
    {
        return new SkillProcess(
        (CardFacade dealer) => { Skill(dealer, targets); }
        );
    }
    public abstract string Text();

}