using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillCardBool : ISkillText
{
    //Skillでfacade状況を判定するためのもの
    bool SkillBool(IPermanent dealableCard);
}
