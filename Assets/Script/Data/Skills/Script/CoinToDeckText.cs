﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
//ここに効果を書き続ける

[CreateAssetMenu(fileName = "Data", menuName = "CardText/CoinToDeckText")]
public class CoinToDeckText : ScriptableUseSkill
{

    [SerializeField] private Coin c;
    [SerializeField] private short amount = 0;
    [SerializeField] private StageDeck deck;
    protected override void Skill(CardDealer dealer, Card target)
    {
        dealer.CoinToDeck(deck, c, amount);
        Debug.Log("CoinToDeck");
    }
    public override bool UseAble(CardDealer dealer, Card source)
    {
        return true;
    }
}
