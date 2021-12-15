using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using System.Linq;

public class EffectLocation
{
    public ICardPrintable source;
    public List<ICardPrintable> target = new List<ICardPrintable>();
    public EffectLocation(ICardPrintable Source)
    {
        this.source = Source;
    }
    public void AddTarget(ICardPrintable printable)
    {
        this.target.Add(printable);
    }
}
