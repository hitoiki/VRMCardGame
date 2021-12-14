using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseRawSkill : IUseProcess
{
    [SerializeReference, SubclassSelector] public IRawSkill skill;
    public void GetSkillProcess(CardFacade facade)
    {
        skill.GetSkillProcess(facade);
    }
    public bool GetIsSkillable(CardFacade facade)
    {
        return true;
    }
    public ICardChecking PlayPrepare()
    {
        return null;
    }

    public string Text()
    {
        return skill.Text();
    }

    public string SkillName()
    {
        return "Use" + skill.SkillName();
    }
}
