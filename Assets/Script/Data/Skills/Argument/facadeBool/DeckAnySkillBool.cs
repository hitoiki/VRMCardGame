using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckAnySkillBool : ISkillFacadeBool
{
    [SerializeReference, SubclassSelector] ISkillCardBool cardBool;
    [SerializeField] DeckType deck;
    public bool SkillBool(CardFacade facade)
    {
        return facade.DeckKey(deck).Any(x => { return cardBool.SkillBool(facade.skillTarget); });
    }
    public string Text()
    {
        return deck.ToCardText() + "に" + cardBool.Text() + "であるカードが存在する";
    }

    public string SkillName()
    {
        return "";
    }
}