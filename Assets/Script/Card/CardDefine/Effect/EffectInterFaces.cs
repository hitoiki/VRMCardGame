using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUseEffect
{
    //手札から使われる能力。
    void Effect(CardDealer dealer);
}


public interface ISelectEffect
{
    void Effect(CardDealer dealer, Card c);
}

public interface IReactiveEffect
{
    //Coinを受け取って発動する能力。
    void Effect(CardDealer dealer, Coin c);
}
