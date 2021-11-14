using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoinProcess : ISkillText
{
    //Coinを受け取って発動する能力。
    SkillProcess GetProcess(Coin c, int n);
}
