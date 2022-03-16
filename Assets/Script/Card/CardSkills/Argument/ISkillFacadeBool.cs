using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillFacadeBool : ISkillText
{
    //Skillでfacade状況を判定するためのもの
    bool SkillBool(CardFacade facade);
}
