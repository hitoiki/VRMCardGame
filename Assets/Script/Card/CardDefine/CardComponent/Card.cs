using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


[System.Serializable]
public class Card : IUseSkill, IDrawSkill, ISelectSkill//,ICoinSkill
{
    // 実際に扱われるカード
    //エフェクトの欄をDataに足す

    public CardData mainData;
    public List<CardData> underCards = new List<CardData>();
    public ReactiveDictionary<Coin, short> coins = new ReactiveDictionary<Coin, short>();

    public Card(CardData d)
    {
        mainData = d;
    }
    //効果が反応するようなCoin操作
    public void AddCoin(CardDealer dealer, Coin c, short n)
    {
        if (coins.ContainsKey(c)) coins[c] += n;
        else coins.Add(c, n);
        CoinSkill(dealer, c, n);
    }

    public void RemoveCoin(CardDealer dealer, Coin c, short n)
    {
        if (coins.ContainsKey(c))
        {
            if (coins[c] > n) coins[c] = 0;
            else coins[c] -= n;
        }
        CoinSkill(dealer, c, n);
    }

    public string CardText()
    {
        string str = "";
        str += mainData.cardName + "「" + mainData.CardText() + "」\n";
        foreach (CardData data in underCards) str += data.cardName + "「" + mainData.CardText() + "」\n";
        return str;
    }

    public bool SelectActive()
    {
        return mainData.selectText != null;
    }

    //CardDetaの"Text"を読み取って効果を発動する
    //CoinはCoinの変更時に
    //Dealerとかで発動タイミングの統括を図ったほうが良いような感じもする
    //非同期処理でないはずなので多分大丈夫きっと恐らく
    public void CoinSkill(CardDealer dealer, Coin coin, short n)
    {
        if (mainData.coinText != null) mainData.coinText.Skill(dealer, this, coin, n);
        foreach (CardData data in underCards) if (data.coinText != null) data.coinText.Skill(dealer, this, coin, n);
    }

    public void UseSkill(CardDealer dealer)
    {
        dealer.TextView(this);
        if (mainData.useText != null) mainData.useText.Skill(dealer, this);
        foreach (CardData data in underCards) if (data.useText != null) data.useText.Skill(dealer, this);
    }

    public void DrawSkill(CardDealer dealer, StageDeck from, StageDeck to)
    {
        dealer.TextView(this);
        if (mainData.drawText != null) mainData.drawText.Skill(dealer, this, from, to);
        foreach (CardData data in underCards) if (data.drawText != null) data.drawText.Skill(dealer, this, from, to);
    }
    public void SelectSkill(CardDealer dealer, Card target)
    {
        dealer.TextView(this);
        if (mainData.selectText != null) mainData.selectText.Skill(dealer, this, target);
        foreach (CardData data in underCards) if (data.selectText != null) data.selectText.Skill(dealer, this, target);
    }

}
