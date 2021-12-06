using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using System.Linq;

public class EffectLocation
{
    private ICardPrintable source;
    private List<ICardPrintable> target = new List<ICardPrintable>();
    public EffectLocation(ICardPrintable Source, List<ICardPrintable> Target)
    {
        this.source = Source;
        if (Target != null) this.target = Target;
    }

    public IObservable<Unit> Effect(ISkillEffect effect)
    {
        return effect.Effect(source, target);
    }

    public void AddTarget(ICardPrintable printable)
    {
        this.target.Add(printable);
    }
}
