using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProcess
{
    //Cardの処理を示すクラス

    public delegate void SkillType(CardFacade dealer);
    public SkillType skill;
    public SkillProcess(SkillType s)
    {
        skill = s;
    }


}