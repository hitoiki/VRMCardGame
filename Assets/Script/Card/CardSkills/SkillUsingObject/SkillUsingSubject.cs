using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using System.Linq;

[System.Serializable]
public class SkillUsingSubject : IDisposable
{
    //Effect,カード選択など一旦待つ為のクラス
    //Skillから命令を受けて、Effectが終わるまでのSubjectを返す
    //Skillから命令を受けて、カード選択の下りを行う

    [SerializeField] private CardSelector selector;
    [SerializeField] private EffectStateLinker linker;
    public Subject<Unit> skillEnd;

    public SkillUsingSubject(SkillUsingObjectAddress address)
    {
        selector = address.selector;
        linker = address.linker;
        skillEnd = new Subject<Unit>();
    }

    public IObservable<Unit> EffectLoad(ISkillEffect[] effects, IPermanent permanent)
    {
        return Observable.Defer<Unit>(() =>
        {
            List<IObservable<Unit>> effectEvents = new List<IObservable<Unit>>();
            IObservable<Unit> observable = Observable.Empty<Unit>();

            foreach (ISkillEffect e in effects.Where(x => { return x != null; }))
            {
                linker.effects.Add(e);
                IObservable<Unit> effectObservable = permanent.GetEffectProjector().EffectBoot(e);
                observable = observable.Concat(effectObservable);
            }
            return observable;
        });
    }


    //別にメソッドに分ける理由ないのだが、分かりやすくするため
    public void SkillEnd()
    {
        Dispose();
    }
    public void Dispose()
    {
        skillEnd.OnCompleted();
        selector = null;
        linker = null;
    }
}
