using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using System.Linq;
using System;


public class SoundEffect : ISkillEffect
{
    [SerializeField] SkillUsingObjectAddress memo;
    [SerializeField] AudioClip audioClip;
    public IObservable<Unit> Effect(EffectLocation location)
    {

        return Observable.Defer<Unit>(() =>
        {
            memo.source.clip = audioClip;
            memo.source.Play();
            return Observable.Empty<Unit>();
        }
        );
    }

    public void Pause()
    {
        memo.source.Pause();
    }

    public void Play()
    {
        memo.source.Play();
    }
}
