using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableCoinSkill : ScriptableObject, ICoinSkill
{
    protected abstract void Skill(CardDealer dealer, Card source, Coin c, short n);

    public CardSkill CoinSkill(Card source, Coin coin, short n)
    {
        return new CardSkill(
        (CardDealer dealer) => { Skill(dealer, source, coin, n); }
        );
    }

    public abstract string Text();
}