using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableUseSkill : ScriptableObject, IUseSkill
{
    protected abstract void Skill(CardDealer dealer, Card source);
    public abstract bool UseAble(CardDealer dealer, Card source);
    public CardSkill UseSkill(Card source)
    {
        return new CardSkill(
        (CardDealer dealer) => { Skill(dealer, source); }
        );
    }
    public abstract string Text();
}
