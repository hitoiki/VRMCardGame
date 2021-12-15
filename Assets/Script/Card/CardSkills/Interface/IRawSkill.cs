using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public interface IRawSkill : ISkillText
{
    //実行効果のみ持つ、生のSkill
    //カードがRawSkillを持つことはないが、他のSkillによって使用される
    IObservable<Unit> GetSkillProcess(CardFacade facade);
}