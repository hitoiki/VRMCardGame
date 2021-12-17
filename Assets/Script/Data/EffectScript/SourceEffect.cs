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

    public IObservable<Unit> Effect(EffectLocation location)
    {

        return Observable.Create<Unit>(observer =>
        {
            effectObj = GameObject.Instantiate(appearObj, location.source.GetTransform().position, Quaternion.identity);
            tween = DOVirtual.DelayedCall(tweenTime, () => { Transform.Destroy(effectObj.gameObject); });
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
        });
    }
    public void Pause()
    {
        tween.Pause();
    }

    public void Play()
    {
        tween.Play();
    }
}
