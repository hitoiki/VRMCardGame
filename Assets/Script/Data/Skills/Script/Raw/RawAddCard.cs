using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawAddCard : IRawSkill
{
    [SerializeField] CardData card;
    [SerializeField] DeckType to;
    public void GetSkillProcess(CardFacade facade)
    {
        facade.AddCard(new DefaultCard(card), to);
    }

    public string Text()
    {
        return StageDeckMethod.ToCardText(to) + "に" + card.cardName + "を加える。";
    }

    public string SkillName()
    {
        return "AddCard:" + StageDeckMethod.ToCardText(to) + "," + card.cardName;
    }
}