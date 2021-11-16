using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawDeckToCoin : IRawSkill
{
    [SerializeField] private Coin c;
    [SerializeField] private short amount = 0;
    [SerializeField] private StageDeck deck;
    private void Skill(CardFacade facade)
    {
        facade.CoinToDeck(deck, c, amount);
    }
    public SkillProcess GetProcess()
    {
        return new SkillProcess(
        (CardFacade dealer) => { Skill(dealer); }
        );
    }

    public string Text()
    {
        return StageDeckMethod.ToCardText(deck) + "の全てのカードに" + c.name + "を" + amount.ToString() + "枚与える。";
    }
}


