using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableCoinSkill : ScriptableObject, ICoinSkill
{
    protected abstract void Skill(CardFacade dealer, Card source, Coin c, short n);

    public SkillProcess CoinSkill(Card source, Coin coin, short n)
    {
        return new SkillProcess(
        (CardFacade dealer) => { Skill(dealer, source, coin, n); }
        );
    }

    public abstract string Text();
}