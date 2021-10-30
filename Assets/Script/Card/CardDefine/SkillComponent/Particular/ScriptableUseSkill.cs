using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableUseSkill : ScriptableObject, IUseSkill
{
    protected abstract void Skill(CardFacade dealer);
    public abstract bool UseAble(Stage data);
    public abstract (StageDeck, sbyte)? SelectCard(Stage data);

    public SkillProcess UseSkill()
    {
        return new SkillProcess(
        (CardFacade dealer) => { Skill(dealer); }
        );
    }
    public abstract string Text();
}
