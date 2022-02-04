using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillCard : ISkillText
{
    //SkillでfacadeからICardを取ってくるもの
    ICard SkillCard(CardFacade facade);
}

