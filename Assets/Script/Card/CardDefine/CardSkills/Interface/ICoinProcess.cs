using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoinProcess : ISkillText
{
    //Coinを受け取って発動する能力。
    void GetSkillProcess(CardFacade facede, Coin c, int n);
    bool GetIsSkillable(CardFacade facede, Coin c, int n);
}
