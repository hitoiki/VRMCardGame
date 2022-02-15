using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillBooledRandomSkillCard : ISkillCard
{
    //条件の合う中からランダム
    [SerializeReference, SubclassSelector] ISkillBool skillBool;
    [SerializeField] DeckType deck;
    public IPermanent SkillCard(CardFacade facade)
    {
        return facade.DeckKey(deck).Where(x => { return skillBool.SkillBool(x); }).OrderBy(a => Guid.NewGuid()).First();
    }
    public string Text()
    {
        return deck.ToCardText() + skillBool.Text() + "";
    }

    public string SkillName()
    {
        return "";
    }

}
