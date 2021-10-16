using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カード能力の形態をinterfaceで纏めておく。
public interface IUseSkill
{
    //手札から使われる能力。
    bool UseAble(CardDealer dealer, Card source);
    CardSkill UseSkill(Card source);
}


public interface ISelectSkill
{
    (StageDeck, sbyte)? SelectCard(CardDealer dealer, Card source);
    CardSkill SelectSkill(Card source, List<Card> c);
}

public interface ICoinSkill
{
    //Coinを受け取って発動する能力。
    CardSkill CoinSkill(Card source, Coin c, short n);
}

public interface IDrawSkill
{
    //Deck間を移動した時に発動する能力。
    CardSkill DrawSkill(Card source, StageDeck from, StageDeck to);
}

//下にカードが追加された時
public interface ILaySkill
{
    //UnderCardに変更があった時に呼ばれる能力…の予定
    CardSkill LaySkill(Card source, List<Card> newUnderCard);
}