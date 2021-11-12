using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using System.Linq;
using System;

[System.Serializable]
public class MovingEffect : ISkillEffect
{
    [SerializeField] GameObject flyingObj;
    [SerializeField] float tweenTime;

    List<GameObject> effects = new List<GameObject>();

    public IObservable<Unit> Effect(SkillTarget target)
    {
        return Effect(target.source, target.target);
    }
    private IObservable<Unit> Effect(IDealableCard Source, IDealableCard[] Target)
    {
        List<IObservable<Unit>> observables = new List<IObservable<Unit>>();

        if (Target != null && Target.Any())
        {
            foreach (Vector3 pos in Target.Select(x => { return x.GetTransform().position; }))
            {
                GameObject copy = GameObject.Instantiate(flyingObj, Source.GetTransform().position, Quaternion.identity);
                Tween tween = copy.transform.DOMove(pos, tweenTime);
                effects.Add(copy);
                observables.Add(Observable.Create<Unit>(observer =>
                {
                    tween.OnComplete(
                     () =>
                     {
                         observer.OnNext(Unit.Default);
                         observer.OnCompleted();
                         GameObject.Destroy(copy);
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
        Debug.Log(effects == null);
        Debug.Log(effects.Any());
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
