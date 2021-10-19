using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSkill
{
    //Cardの処理を示すクラス

    public delegate void SkillType(CardDealer dealer);
    public SkillType skill;
    public CardSkill(SkillType s)
    {
        skill = s;
    }


}