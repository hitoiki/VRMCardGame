using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDrawProcess : ISkillText
{
    //Deck間を移動した時に発動する能力。
    void GetSkillProcess(CardFacade facade, IDeck from, IDeck to);
    bool GetIsSkillable(CardFacade facade, IDeck from, IDeck to);
}