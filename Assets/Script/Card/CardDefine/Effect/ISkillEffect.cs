using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;


public interface ISkillEffect
{
    // Skillによって生じるエフェクト効果を書くinterFace
    // IObservableのOnCompleteを監視して、Card処理が進行する
    IObservable<Unit> Effect(SkillTarget target);

}
