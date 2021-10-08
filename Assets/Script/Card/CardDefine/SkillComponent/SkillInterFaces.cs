using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUseSkill
{
    //手札から使われる能力。
    void UseSkill(CardDealer dealer);
}


public interface ISelectSkill
{
    void SelectSkill(CardDealer dealer, Card c);
}

public interface ICoinSkill
{
    //Coinを受け取って発動する能力。
    void CoinSkill(CardDealer dealer, Coin c, short n);
}

public interface IDrawSkill
{
    //Deck間を移動した時に発動する能力。
    void DrawSkill(CardDealer dealer, StageDeck from, StageDeck to);
}

//下にカードが追加された時