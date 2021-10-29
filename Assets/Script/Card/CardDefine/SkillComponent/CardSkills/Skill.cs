using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public SkillEffect effect;
    public SkillProcess process;

    public Skill(SkillEffect Effect, SkillProcess Process)
    {
        this.effect = Effect;
        this.process = Process;

    }
}
