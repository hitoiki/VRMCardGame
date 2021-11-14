using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カード能力の形態をinterfaceで纏めておく。
//ここのinterfaceが示すのは、
public interface IUseProcess : ISkillText
{
    //手札から使われる能力。
    //カード選択が必要かどうかも同時に判定する。
    bool UseAble(Stage data);
    ICardChecking PlayPrepare(Stage data);
    SkillProcess GetProcess();
}

public interface ICoinProcess : ISkillText
{
    //Coinを受け取って発動する能力。
    SkillProcess GetProcess(Coin c, int n);
}

public interface IDrawProcess : ISkillText
{
    //Deck間を移動した時に発動する能力。
    SkillProcess GetProcess(StageDeck from, StageDeck to);
}

//下にカードが追加された時
public interface ILayProcess : ISkillText
{
    //UnderCardに変更があった時に呼ばれる能力…の予定
    SkillProcess LaySkill(List<Card> newUnderCard);
}