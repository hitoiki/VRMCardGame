using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UniRx;

public class TemplateEffect : ISkillEffect
{
    [SerializeField] EffectTemplate template;
    List<ISkillEffect> effectEvents = new List<ISkillEffect>();

    public IObservable<Unit> Effect(EffectLocation location)
    {

        return Observable.Defer<Unit>(() =>
        {
            effectEvents = new List<ISkillEffect>();
            IObservable<Unit> observable = Observable.Empty<Unit>();

            foreach (ISkillEffect e in template.effect)
            {
                effectEvents.Add(e);
                IObservable<Unit> effectObservable = e.Effect(location);
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
