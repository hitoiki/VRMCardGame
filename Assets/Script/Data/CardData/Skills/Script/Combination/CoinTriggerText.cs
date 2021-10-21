﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
[CreateAssetMenu(fileName = "Data", menuName = "CardText/CoinTriggerText")]
public class CoinTriggerText : ScriptableCoinSkill
{
    [SerializeField] private Coin ReactiveCoin;
    [SerializeField] private short threshold = 0;
    [SerializeField] private ScriptableUseSkill useText;

    protected override void Skill(CardFacade dealer, Card source, Coin c, short n)
    {
        if (source.coins[ReactiveCoin] >= threshold)
        {
            useText.UseSkill(source).skill(dealer);
        }
    }

    public override string Text()
    {
        return ReactiveCoin.name + "が" + threshold.ToString() + "以上になった時、" + useText.Text();
    }

}