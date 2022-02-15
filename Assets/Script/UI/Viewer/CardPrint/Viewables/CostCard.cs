using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CostCard : MonoBehaviour, ICardViewable
{
    //Costを表示するためのViewable
    [SerializeField] CoinSprite coinSprite;
    public void Active(bool b)
    {
        this.gameObject.SetActive(b);
    }
    public void UnPrint()
    {

    }

    public void Print(IPermanent c)
    {
        if (c.GetCardData().costCoin != null) coinSprite.CoinPrint(c.GetCardData().costCoin, c.GetCardData().cost);
    }
}
