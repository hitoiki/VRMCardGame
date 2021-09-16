using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCard : MonoBehaviour, ICardPrinted
{
    //CardのCoinを表示するやつ
    private Card card;

    public void Print(Card c)
    {
        card = c;

    }

    public void Active(bool b)
    {
        this.gameObject.SetActive(b);
    }
}
