using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticSkills
{
    public static Skill IdentitySkill = new Skill("", new ISkillEffect[0], (x) => { }, x => { return true; });
}


