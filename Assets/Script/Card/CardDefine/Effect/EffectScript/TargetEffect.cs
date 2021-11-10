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
    List<GameObject> effects = new List<GameObject>();

    public IObservable<Unit> Effect(SkillTarget target)
    {
        List<IObservable<Unit>> observables = new List<IObservable<Unit>>();

        if (target.target != null && target.target.Any())
        {
            foreach (Vector3 pos in target.target.Select(x => { return x.GetTransform().position; }))
            {
                GameObject copy = GameObject.Instantiate(appearObj, pos, Quaternion.identity);
                Tween tween = DOVirtual.DelayedCall(3, () => { Transform.Destroy(copy.gameObject); });
                effects.Add(copy);
                observables.Add(Observable.Create<Unit>(observer2 =>
                {
                    tween.OnComplete(
                     () =>
                     {
                         observer2.OnNext(Unit.Default);
                         observer2.OnCompleted();
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
        foreach (Transform t in effects.Select(x => { return x.GetComponent<Transform>(); }))
        {
            t.DOPause();
        }
    }

    public void Play()
    {
        foreach (Transform t in effects.Select(x => { return x.GetComponent<Transform>(); }))
        {
            t.DOPlay();
        }
    }
}
