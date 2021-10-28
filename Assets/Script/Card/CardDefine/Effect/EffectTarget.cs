using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTarget
{
    public IDealableCard source;
    public IDealableCard[] target;
    public EffectTarget(IDealableCard Source, IDealableCard[] Target)
    {
        this.source = Source;
        this.target = Target;
    }
}

