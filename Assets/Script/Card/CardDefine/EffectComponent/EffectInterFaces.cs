using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUseEffect
{
    //手札から使われる能力。
    void UseEffect(CardDealer dealer);
}


public interface ISelectEffect
{
    void SelectEffect(CardDealer dealer, Card c);
}

public interface ICoinEffect
{
    //Coinを受け取って発動する能力。
    void CoinEffect(CardDealer dealer, Coin c, short n);
}

public interface IDrawEffect
{
    //Deck間を移動した時に発動する能力。
    void DrawEffect(CardDealer dealer, StageDeck from, StageDeck to);
}

//下にカードが追加された時