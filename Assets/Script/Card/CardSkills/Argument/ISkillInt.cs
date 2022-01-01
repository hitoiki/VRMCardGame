using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillInt : ISkillText
{
    //SkillでfacadeからIntを取ってくるもの
    int SkillInt(CardFacade facade);
}
