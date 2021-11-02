using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カード能力の形態をinterfaceで纏めておく。
public interface IUseSkill
{
    //手札から使われる能力。
    //カード選択が必要かどうかも同時に判定する。
    bool UseAble(Stage data);
    (StageDeck, sbyte)? SelectCard(Stage data);
    SkillProcess UseSkill();
    string Text();
}

public interface ICoinSkill
{
    //Coinを受け取って発動する能力。
    SkillProcess CoinSkill(Coin c, int n);
    string Text();
}

public interface IDrawSkill
{
    //Deck間を移動した時に発動する能力。
    SkillProcess DrawSkill(StageDeck from, StageDeck to);
    string Text();
}

//下にカードが追加された時
public interface ILaySkill
{
    //UnderCardに変更があった時に呼ばれる能力…の予定
    SkillProcess LaySkill(List<Card> newUnderCard);
}