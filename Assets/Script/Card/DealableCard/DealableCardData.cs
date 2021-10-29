using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealableCardData
{
    //IDealableCardを読み取るデータに纏めたクラス

    readonly public Dictionary<Coin, short> coins;

    public DealableCardData(IDealableCard card)
    {
        coins = card.GetCoin().coins;
    }
}
