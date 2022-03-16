using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UniRx;

public class TemplateEffect : ISkillEffect
{
    [SerializeField] EffectTemplate template;
    List<IObservable<Unit>> effectEvents = new List<IObservable<Unit>>();

    public IObservable<Unit> Effect(EffectLocation location)
    {

        return Observable.Defer<Unit>(() =>
        {
            effectEvents = new List<IObservable<Unit>>();
            IObservable<Unit> observable = Observable.Empty<Unit>();

            foreach (ISkillEffect e in template.effect)
            {
                IObservable<Unit> effectObservable = e.Effect(location);
                effectEvents.Add(effectObservable);
                observable = observable.Concat(effectObservable);
            }
            return observable;
        });
    }
    public void Pause()
    {
        if (!effectEvents.Any()) return;
        foreach (ISkillEffect e in effectEvents)
        {
            e.Pause();
        }
    }

    public void Play()
    {
        if (!effectEvents.Any()) return;
        foreach (ISkillEffect e in effectEvents)
        {
            e.Play();
        }
    }
}
