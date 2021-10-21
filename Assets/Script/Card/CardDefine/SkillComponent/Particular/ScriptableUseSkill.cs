using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableUseSkill : ScriptableObject, IUseSkill
{
    protected abstract void Skill(CardFacade dealer, Card source);
    public abstract bool UseAble(GamePlayData data, Card source);
    public SkillProcess UseSkill(Card source)
    {
        return new SkillProcess(
        (CardFacade dealer) => { Skill(dealer, source); }
        );
    }
    public abstract string Text();
}
