using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTarget
{
    public ICardPrintable source;
    public ICardPrintable[] target;
    public EffectTarget(ICardPrintable Source, ICardPrintable[] Target)
    {
        this.source = Source;
        this.target = Target;
    }

}
