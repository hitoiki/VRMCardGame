using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableCoinSkill : ScriptableObject, ICoinSkill
{
    protected abstract void Skill(CardFacade dealer, Coin c, short n);

    public SkillProcess CoinSkill(Coin coin, short n)
    {
        return new SkillProcess(
        (CardFacade dealer) => { Skill(dealer, coin, n); }
        );
    }

    public abstract string Text();
}