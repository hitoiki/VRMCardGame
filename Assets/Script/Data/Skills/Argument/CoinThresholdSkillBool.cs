using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinThresholdSkillBool : ISkillBool
{
    [SerializeField] private Coin coin;
    [SerializeField] private int threshold;
    public bool SkillBool(CardFacade facade)
    {
        return facade.skillTarget.GetCoin()[coin] >= threshold;
    }
    public string Text()
    {
        return coin.name + "が" + threshold.ToString() + "以上である";
    }

    public string SkillName()
    {
        return "";
    }
}
