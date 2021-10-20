using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableDrawSkill : ScriptableObject, IDrawSkill
{

    protected abstract void Skill(CardFacade dealer, Card source, StageDeck from, StageDeck to);

    public CardSkill DrawSkill(Card source, StageDeck from, StageDeck to)
    {
        return new CardSkill(
        (CardFacade dealer) => { Skill(dealer, source, from, to); }
        );
    }
    public abstract string Text();
}

