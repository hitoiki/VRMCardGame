using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
//ここに効果を書き続ける

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
