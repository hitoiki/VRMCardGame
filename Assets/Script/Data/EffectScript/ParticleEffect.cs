using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using UniRx;
using System;
using DG.Tweening;


public class ParticleEffect : ISkillEffect
{
    [SerializeField] ParticleSystem initParticle;
    [SerializeField] float tweenTime;
    private Tween tween;
    private GameObject effectObj;
    private ParticleSystem effectParticle;

    public IObservable<Unit> Effect(EffectLocation location)
    {

        return Observable.Create<Unit>(observer =>
        {
            effectObj = GameObject.Instantiate(initParticle.gameObject, location.source.GetTransform().position + Vector3.back, Quaternion.identity);
            effectParticle = effectObj.GetComponent<ParticleSystem>();
            tween = DOVirtual.DelayedCall(tweenTime, () => { effectParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting); Transform.Destroy(effectObj.gameObject); });
            effectParticle.Play();
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
        effectParticle.Pause();
    }

    public void Play()
    {
        tween.Play();
        effectParticle.Play();
    }
}
