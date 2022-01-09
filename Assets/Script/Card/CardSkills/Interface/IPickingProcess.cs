using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public interface IPickingProcess : ISkillText
{
    //Coinを受け取って発動する能力。
    IObservable<Unit> GetSkillProcess(CardFacade facede, SkillDealableCard dealableCard);
    bool GetIsSkillable(CardFacade facede, SkillDealableCard dealableCard);
}

