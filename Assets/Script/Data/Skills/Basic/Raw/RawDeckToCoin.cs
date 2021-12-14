using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawDeckToCoin : IRawSkill
{
    [SerializeField] private Coin c;
    [SerializeField] private short amount = 0;
    [SerializeField] private DeckType deck;
    public void GetSkillProcess(CardFacade facade)
    {
        foreach (SkillDealableCard card in facade.FieldDeck())
        {
            card.ChangeCoin(c, amount);
        }
    }

    public string Text()
    {
        return StageDeckMethod.ToCardText(deck) + "の全てのカードに" + c.name + "を" + amount.ToString() + "枚与える。";
    }

    public string SkillName()
    {
        return "DeckToCoin(" + StageDeckMethod.ToCardText(deck) + "," + c.coinName + "," + amount.ToString() + ")";
    }
}


