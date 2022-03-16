using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPoint : ISkillProcessTag
{

    [SerializeReference, SubclassSelector] private ISkillInt number;

    public int GetVictoryPoint(CardFacade facade)
    {
        return number.SkillInt(facade);
    }
    public string Text()
    {
        return "勝利点" + number.Text();
    }

    public string SkillName()
    {
        return "VictoryPoint(" + number.SkillName() + ")";
    }
}
