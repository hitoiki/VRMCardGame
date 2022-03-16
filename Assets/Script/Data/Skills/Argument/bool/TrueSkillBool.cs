using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueSkillBool : ISkillCardBool
{
    public bool SkillBool(IPermanent dealableCard)
    {
        return true;
    }
    public string Text()
    {
        return "任意の";
    }

    public string SkillName()
    {
        return "";
    }
}

