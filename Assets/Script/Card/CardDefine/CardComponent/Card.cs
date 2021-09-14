using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Card : IUseEffect, IDrawEffect, ISelectEffect//,ICoinEffect
{
    // 実際に扱われるカード
    //エフェクトの欄をDataに足す

    public CardData mainData;
    public List<CardData> underCards = new List<CardData>();
    public Dictionary<Coin, short> coins { get; } = new Dictionary<Coin, short>();

    public Card(CardData d)
    {
        mainData = d;
    }
    //効果が反応するようなCoin操作
    public void AddCoin(CardDealer dealer, Coin c, short n)
    {
        if (coins.ContainsKey(c)) coins[c] += n;
        else coins.Add(c, n);
        CoinEffect(dealer, c, n);
    }

    public void RemoveCoin(CardDealer dealer, Coin c, short n)
    {
        if (coins.ContainsKey(c))
        {
            if (coins[c] > n) coins.Remove(c);
            else coins[c] -= n;
        }
        CoinEffect(dealer, c, n);
    }

    public string CardText()
    {
        string str = "";
        str += mainData.cardName + "「" + mainData.CardText() + "」\n";
        foreach (CardData data in underCards) str += data.cardName + "「" + mainData.CardText() + "」\n";
        return str;
    }

    //CardDetaの"Text"を読み取って効果を発動する
    //CoinはCoinの変更時に
    //Dealerとかで発動タイミングの統括を図ったほうが良いような感じもする
    //非同期処理でないはずなので多分大丈夫きっと恐らく
    public void CoinEffect(CardDealer dealer, Coin coin, short n)
    {
        if (mainData.coinText != null) mainData.coinText.Effect(dealer, this, coin, n);
        foreach (CardData data in underCards) if (data.coinText != null) data.coinText.Effect(dealer, this, coin, n);
    }

    public void UseEffect(CardDealer dealer)
    {
        dealer.TextView(this);
        if (mainData.useText != null) mainData.useText.Effect(dealer, this);
        foreach (CardData data in underCards) if (data.useText != null) data.useText.Effect(dealer, this);
    }

    public void DrawEffect(CardDealer dealer, StageDeck from, StageDeck to)
    {
        dealer.TextView(this);
        if (mainData.drawText != null) mainData.drawText.Effect(dealer, this, from, to);
        foreach (CardData data in underCards) if (data.drawText != null) data.drawText.Effect(dealer, this, from, to);
    }
    public void SelectEffect(CardDealer dealer, Card target)
    {
        dealer.TextView(this);
        if (mainData.selectText != null) mainData.selectText.Effect(dealer, this, target);
        foreach (CardData data in underCards) if (data.selectText != null) data.selectText.Effect(dealer, this, target);
    }

}
