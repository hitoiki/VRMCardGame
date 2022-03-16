using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinThresholdSkillBool : ISkillCardBool
{
    [SerializeField] private Coin coin;
    [SerializeField] private int threshold;
    [SerializeField] private ComparisonEnum equalSign;
    public bool SkillBool(IPermanent dealableCard)
    {
        if (!dealableCard.GetCoin().ContainsKey(coin)) return false;
        return equalSign.Check(dealableCard.GetCoin()[coin], threshold);
    }
    public string Text()
    {
        return equalSign.CardText(coin.name, threshold.ToString());
    }

    public string SkillName()
    {
        return "";
    }
}
