using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UniRx;

public class EffectProjector
{
    //Effectを描写するPrintable等を握って置き、SKill上でそれをやり取りできるようにするクラス
    private IList<ICardPrintable> effectLoaderList;
    public EffectProjector()
    {
        effectLoaderList = new List<ICardPrintable>();
    }
    public EffectProjector(List<ICardPrintable> print)
    {

        effectLoaderList = print;
    }
    //Effectが起こった際に購読する
    public void EffectSubScribe(ICardPrintable print)
    {
        effectLoaderList.Add(print);
    }
    //購読解除
    public void EffectUnSubScribe(ICardPrintable print)
    {
        effectLoaderList.Remove(print);
    }
    //実行
    public IObservable<Unit> EffectBoot(ISkillEffect effect)
    {
        return Observable.Defer<Unit>(() =>
       {
           if (effectLoaderList == null) return Observable.Empty<Unit>();
           return Observable.Concat<Unit>(effectLoaderList.Select(x => { return effect.Effect(new EffectLocation(x)); }));
       });
    }

    public static EffectProjector operator +(EffectProjector x, EffectProjector y)
    {
        return new EffectProjector(x.effectLoaderList.Concat(y.effectLoaderList).ToList());
    }
}
