using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillBool : ISkillText
{
    //Skillでfacade状況を判定するためのもの
    bool SkillBool(ICard dealableCard);
}
