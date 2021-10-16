using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableSelectSkill : ScriptableObject, ISelectSkill
{
    [SerializeField] private SkillPriority priority;
    [SerializeField] private SkillPhase phase;
    protected abstract void Skill(CardDealer dealer, Card source, List<Card> targets);
    public abstract (StageDeck, sbyte)? SelectCard(CardDealer dealer, Card source);

    public CardSkill SelectSkill(Card source, List<Card> targets)
    {
        return new CardSkill(priority, phase,
        (CardDealer dealer) => { Skill(dealer, source, targets); }
        );
    }
}