using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カード能力の形態をinterfaceで纏めておく。
//ここのinterfaceが示すのは処理のみ
public interface IUseProcess : ISkillText
{
    //手札から使われる能力。
    //カード選択が必要かどうかも同時に判定する。
    IsSkillable GetIsSkillable();
    ICardChecking PlayPrepare(Stage data);
    SkillProcess GetProcess();
}