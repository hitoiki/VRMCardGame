using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSkill
{
    //Cardの処理を示すクラス

    public delegate void SkillType(CardFacade dealer);
    public SkillType skill;
    public CardSkill(SkillType s)
    {
        skill = s;
    }


}