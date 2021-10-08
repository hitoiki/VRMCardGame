using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
[CreateAssetMenu(fileName = "Data", menuName = "CardText/TextSet/CoinTextSet")]
public class CoinTextSet : CoinText
{
    [SerializeField] private CoinText[] texts = null;

    public override string Text()
    {
        string str = "";
        foreach (CoinText t in texts)
        {
            str += "\n" + t.Text();
        }
        return str;
    }
    public override void Skill(CardDealer dealer, Card target, Coin c, short n)
    {
        foreach (CoinText t in texts) t.Skill(dealer, target, c, n);
    }
}