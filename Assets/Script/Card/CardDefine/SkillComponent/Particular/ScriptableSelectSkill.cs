﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableSelectSkill : ScriptableObject, ISelectSkill
{
    protected abstract void Skill(CardFacade dealer, Card source, List<Card> targets);
    public abstract (StageDeck, sbyte) SelectCard(GamePlayData data, Card source);

    public CardSkill SelectSkill(Card source, List<Card> targets)
    {
        return new CardSkill(
        (CardFacade dealer) => { Skill(dealer, source, targets); }
        );
    }
    public abstract string Text();

}