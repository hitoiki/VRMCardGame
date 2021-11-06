using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public ISkillEffect[] effect;
    public SkillProcess process;

    public Skill(ISkillEffect[] Effect, SkillProcess Process)
    {
        this.effect = Effect;
        this.process = Process;

    }
}
