using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
[CreateAssetMenu(fileName = "Data", menuName = "CardText/CoinTriggerText")]
public class CoinTriggerText : CoinText
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
