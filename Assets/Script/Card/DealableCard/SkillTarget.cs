using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTarget
{
    public IDealableCard source;
    public IDealableCard[] target;
    public SkillTarget(IDealableCard Source, IDealableCard[] Target)
    {
        this.source = Source;
        this.target = Target;
    }
}

