using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using DG.Tweening;

public class MoveEffect : ISkillEffect
{
    [SerializeField] float tweenTime;
    [SerializeField] bool isRelative;
    [SerializeField] Vector3 moveVector;
    private Tween tween;

    public IObservable<Unit> Effect(EffectLocation location)
    {

        return Observable.Create<Unit>(observer =>
        {
            Tween tween;
            if (isRelative) tween = location.source.GetTransform().DOLocalMove(moveVector, tweenTime);
            else tween = location.source.GetTransform().DOMove(moveVector, tweenTime);
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