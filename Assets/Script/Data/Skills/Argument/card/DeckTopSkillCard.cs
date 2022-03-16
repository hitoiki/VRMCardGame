using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckTopSkillCard : ISkillCard
{
    //デッキの上
    [SerializeField] DeckType deck;
    public IPermanent SkillCard(CardFacade facade)
    {
        return facade.DeckKey(deck).First();
    }
    public string Text()
    {
        return deck.ToCardText() + "の一番上";
    }

    public string SkillName()
    {
        return "";
    }

}
