using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
//ここに効果を書き続ける

[CreateAssetMenu(fileName = "Data", menuName = "CardText/BasicReactiveDraw")]
public class BasicReactiveDraw : CoinText
{
    [SerializeField] private Coin ReactiveCoin;
    [SerializeField] private short threshold = 0;
    [SerializeField] private StageDeck drawFrom;
    [SerializeField] private StageDeck drawTo;
    [SerializeField] private short drawAmount = 0;
    public override void Effect(CardDealer dealer, Card target, Coin c, short n)
    {
        if (target.coins[ReactiveCoin] >= threshold)
        {
            dealer.DeckDraw(drawFrom, drawTo, drawAmount);
        }
    }
    public override string Text(){
        return ReactiveCoin.coinName +" "+ threshold.ToString()+":カードを"
        +StageDeckMethod.ToCardText(drawFrom)+"から"+StageDeckMethod.ToCardText(drawTo)
        +"へ"+drawAmount.ToString()+"枚引く。";
    }
}

public class CoinTriggerUseText : CoinText
{
    [SerializeField] private Coin ReactiveCoin;
    [SerializeField] private short threshold = 0;
    [SerializeField] private UseText useText;

    public override void Effect(CardDealer dealer, Card target, Coin c, short n)
    {
        if (target.coins[ReactiveCoin] >= threshold)
        {
            useText.Effect(dealer,target);
        }
    }
    public override string Text(){
        return ReactiveCoin.coinName +" "+ threshold.ToString()+":"+useText.Text();
    }
}

[CreateAssetMenu(fileName = "Data", menuName = "CardText/CoinToDeckText")]
public class CoinToDeckText : UseText{

    [SerializeField] private Coin c;
    [SerializeField] private short amount = 0;
    [SerializeField] private StageDeck deck;
    public override void Effect(CardDealer dealer, Card target)
    {
        dealer.CoinToDeck(deck,c,amount);
        Debug.Log("CoinToDeck");
    }
    public override string Text(){
        return StageDeckMethod.ToCardText(deck) + "全体に" + c.coinName +"を"+amount.ToString() + "つ与える。";
    }
}
