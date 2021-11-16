using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class HighOrderRule
{
    public List<IHighOrderSkill> highOrderSkills;

    public Skill HighOrderApply(Skill skill)
    {
        Skill applyedSkill = skill;
        foreach (IHighOrderSkill highOrder in highOrderSkills.Where(x => { return x.SkillCheck(skill); }))
        {
            applyedSkill = highOrder.HighOrderSkill(applyedSkill);
        }
        return applyedSkill;

    }
}
