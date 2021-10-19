using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillCondition
{
    //Skillがどのような状況で発動されるのかを示すクラス
    //場のカードの枚数がいくつなら～と言った物でなく、
    [SerializeField] public SkillPhase activePhase;

}

public enum SkillPhase
{
    //underCardかを示すenum
    always, top, under
}