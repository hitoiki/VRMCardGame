using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillTarget
{
    public ICardPrintable source;
    public ICardPrintable[] target;

    public StageDeck sourceBelongDeck;
    public StageDeck[] targetBelongDeck;
    public SkillTarget(ICardPrintable Source, StageDeck SourceBelongDeck, ICardPrintable[] Target, StageDeck[] TargetBelongDeck)
    {
        this.source = Source;
        this.target = Target;
        this.sourceBelongDeck = SourceBelongDeck;
        this.targetBelongDeck = TargetBelongDeck;
    }

    public EffectTarget GetEffectTarget()
    {
        return new EffectTarget(source, target);
    }

    public static SkillTarget SourceOnly(ICardPrintable Source, StageDeck SourceBelongDeck)
    {
        return new SkillTarget(Source, SourceBelongDeck, null, null);
    }
}

