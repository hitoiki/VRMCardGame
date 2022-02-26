using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class TemplateEffect : ISkillEffect
{
    [SerializeField] EffectTemplate template;
    List<IObservable<Unit>> effectEvents;

    public IObservable<Unit> Effect(EffectLocation location)
    {

        return Observable.Defer<Unit>(() =>
        {
            effectEvents = new List<IObservable<Unit>>();

            foreach (ISkillEffect e in template.effect)
            {
                effectEvents.Add(e.Effect(location));
            }
            Observable.WhenAll(effectEvents).First().Subscribe(x => { },
              () =>
              {
                  effectEvents = new List<IObservable<Unit>>();
              });
            return Observable.WhenAll(effectEvents).First();
        });
    }
    public void Pause()
    {
        foreach (ISkillEffect e in effectEvents)
        {
            e.Pause();
        }
    }

    public void Play()
    {
        foreach (ISkillEffect e in effectEvents)
        {
            e.Play();
        }
    }
}
