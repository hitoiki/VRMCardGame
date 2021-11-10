using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using DG.Tweening;

public class SourceEffect : ISkillEffect
{
    [SerializeField] GameObject appearObj;
    [SerializeField] float tweenTime;
    private Tween tween;
    private GameObject effectObj;

    public IObservable<Unit> Effect(SkillTarget target)
    {
        effectObj = GameObject.Instantiate(appearObj, target.source.GetTransform().position, Quaternion.identity);
        tween = DOVirtual.DelayedCall(3, () => { Transform.Destroy(effectObj.gameObject); });
        return Observable.Create<Unit>(observer2 =>
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
        });
    }
    public void Pause()
    {
        effectObj.GetComponent<Transform>().DOPause();
    }

    public void Play()
    {
        effectObj.GetComponent<Transform>().DOPlay();
    }
}
