using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EffectTarget
{
    public ICardPrintable source;
    public ICardPrintable[] target;
    public EffectTarget(ICardPrintable Source, ICardPrintable[] Target)
    {
        this.source = Source;
        this.target = Target;
    }

    public IDealableCard DealableSource()
    {
        return source.GetDealableCard();
    }
    public IDealableCard[] DealableTarget()
    {
        return target.Select(x => { return x.GetDealableCard(); }).ToArray();
    }
}

