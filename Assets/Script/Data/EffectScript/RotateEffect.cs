using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using DG.Tweening;


public class RotateEffect : ISkillEffect
{
    [SerializeField] Vector3 rotateVector;
    [SerializeField] bool isRelative;
    [SerializeField] float tweenTime;
    private Tween tween;

    public IObservable<Unit> Effect(EffectLocation location)
    {

        return Observable.Create<Unit>(observer =>
        {
            if (isRelative) tween = location.source.GetTransform().DOLocalRotate(rotateVector, tweenTime);
            else tween = location.source.GetTransform().DORotate(rotateVector, tweenTime);
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