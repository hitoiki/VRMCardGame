using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Skill
{
    public ISkillEffect[] effect;
    public SkillProcess process;
    public IsSkillable skillable;

    public Skill(ISkillEffect[] Effect, SkillProcess Process, IsSkillable Skillable)
    {
        this.effect = Effect;
        this.process = Process;
        this.skillable = Skillable;
    }

    public static Skill operator +(Skill x, Skill y)
    {
        return new Skill(x.effect.Concat(y.effect).ToArray(), x.process + y.process, facade => { return x.skillable(facade) && y.skillable(facade); });
    }
}
