using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceCoinSkillInt : ISkillInt
{
    [SerializeField] private Coin coin;
    public int SkillInt(CardFacade facade)
    {
        return facade.skillTarget.GetCoin()[coin];
    }
    public string Text()
    {
        return coin.name + "の数";
    }

    public string SkillName()
    {
        return coin.name + "の数";
    }
}
