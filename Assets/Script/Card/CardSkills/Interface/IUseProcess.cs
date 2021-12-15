using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

//カード能力の形態をinterfaceで纏めておく。
//ここのinterfaceが示すのは処理のみ
public interface IUseProcess : ISkillText
{
    //手札から使われる能力。
    //カード選択が必要かどうかも同時に判定する。
    IObservable<Unit> GetSkillProcess(CardFacade facede);
    bool GetIsSkillable(CardFacade facede);
}