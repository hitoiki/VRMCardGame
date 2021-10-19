using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableSelectSkill : ScriptableObject, ISelectSkill
{
    protected abstract void Skill(CardDealer dealer, Card source, List<Card> targets);
    public abstract (StageDeck, sbyte) SelectCard(CardDealer dealer, Card source);

    public CardSkill SelectSkill(Card source, List<Card> targets)
    {
        return new CardSkill(
        (CardDealer dealer) => { Skill(dealer, source, targets); }
        );
    }
    public abstract string Text();

}