using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    // 実際に扱われるカード
    //エフェクトの欄をDataに足す

    public CardData data;
    public Dictionary<Coin, short> coins;

    public Card(CardData d)
    {
        data = d;
    }

    public void AddCoin(Coin c, short n)
    {
        if (coins.ContainsKey(c)) coins[c] += n;
        else coins.Add(c, n);

    }

    public List<IUseEffect> UseEffect()
    {
        return data.useEffects;
    }

}
