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
                Tween tweener = copy.transform.DOMove(pos, tweenTime);
                observables.Add(Observable.Create<Unit>(observer2 =>
                {
                    tweener.OnComplete(
                     () =>
                     {
                         observer2.OnNext(Unit.Default);
                         observer2.OnCompleted();
                         GameObject.Destroy(copy);
                     });
                    return Disposable.Create(() =>
                    {
                        tweener.Kill();
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

}
