using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using DG.Tweening;

public class ResetEffect : ISkillEffect
{
    [SerializeField] float tweenTime;
    private Tween tween;
    private Sequence sequence;

    public IObservable<Unit> Effect(EffectLocation location)
    {

        return Observable.Create<Unit>(observer =>
        {
            sequence = DOTween.Sequence()
              .Append(location.source.GetTransform().DOMove(location.source.GetAnchor(), tweenTime))
              .Join(location.source.GetTransform().DORotate(Vector3.zero, tweenTime));
            sequence.Play();
            sequence.OnComplete(
             () =>
             {
                 observer.OnNext(Unit.Default);
                 observer.OnCompleted();
             });
            return Disposable.Create(() =>
            {
                sequence.Kill();
            });
        });
    }
    public void Pause()
    {
        sequence.Pause();
    }

    public void Play()
    {
        sequence.Play();
    }
}