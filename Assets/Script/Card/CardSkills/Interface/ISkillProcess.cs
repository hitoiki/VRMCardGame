using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public interface ISkillProcess<in T> : ISkillText
{
    IObservable<Unit> GetSkillProcess(CardFacade facede, T t);
    bool GetIsSkillable(CardFacade facede, T t);
}
