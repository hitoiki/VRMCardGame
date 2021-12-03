using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using System.Linq;
using System;


public class SoundEffect : ISkillEffect
{
    [SerializeField] ObjectAddress memo;
    [SerializeField] AudioClip audioClip;
    public IObservable<Unit> Effect(ICardPrintable source, List<ICardPrintable> target)
    {
        memo.source.clip = audioClip;
        memo.source.Play();
        return Observable.Empty<Unit>();
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
