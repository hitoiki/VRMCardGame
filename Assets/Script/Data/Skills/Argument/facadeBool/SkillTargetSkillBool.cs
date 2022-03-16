using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTargetSkillBool : ISkillFacadeBool
{
    [SerializeReference, SubclassSelector] ISkillCardBool cardBool;
    public bool SkillBool(CardFacade facade)
    {
        return cardBool.SkillBool(facade.skillTarget);
    }
    public string Text()
    {
        return "そのカードが" + cardBool.Text();
    }

    public string SkillName()
    {
        return "";
    }
}
