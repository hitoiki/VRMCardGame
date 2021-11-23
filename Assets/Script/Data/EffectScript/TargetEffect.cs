using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using System;
using DG.Tweening;


public class TargetEffect : ISkillEffect
{
    [SerializeField] GameObject appearObj;
    [SerializeField] float tweenTime;
    List<Tween> effects = new List<Tween>();

    public IObservable<Unit> Effect(EffectTarget target)
    {
        List<IObservable<Unit>> observables = new List<IObservable<Unit>>();

        if (target.target != null && target.target.Any())
        {
            foreach (Vector3 pos in target.target.Select(x => { return x.GetTransform().position; }))
            {
                GameObject copy = GameObject.Instantiate(appearObj, pos, Quaternion.identity);
                Tween tween = DOVirtual.DelayedCall(tweenTime, () => { Transform.Destroy(copy.gameObject); });
                effects.Add(tween);
                observables.Add(Observable.Create<Unit>(observer =>
                {
                    tween.OnComplete(
                     () =>
                     {
                         observer.OnNext(Unit.Default);
                         observer.OnCompleted();
                     });
                    return Disposable.Create(() =>
                    {
                        tween.Kill();
                    });
                }));
            }
        }
        if (!observables.Any()) return Observable.Create<Unit>(x =>
        {
            x.OnNext(Unit.Default);
            x.OnCompleted();
            return Disposable.Empty;
        });
        return Observable.WhenAll(observables).First();
    }
    public void Pause()
    {
        foreach (Tween t in effects)
        {
            t.Pause();
        }
    }

    public void Play()
    {
        foreach (Tween t in effects)
        {
            t.Play();
        }
    }
}
