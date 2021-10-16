using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
[CreateAssetMenu(fileName = "Data", menuName = "CardText/BasicReactiveDraw")]
public class BasicReactiveDraw : ScriptableCoinSkill
{
    [SerializeField] private Coin ReactiveCoin;
    [SerializeField] private short threshold = 0;
    [SerializeField] private StageDeck drawFrom;
    [SerializeField] private StageDeck drawTo;
    [SerializeField] private short drawAmount = 0;
    protected override void Skill(CardDealer dealer, Card target, Coin c, short n)
    {
        if (target.coins.ContainsKey(ReactiveCoin) && target.coins[ReactiveCoin] >= threshold)
        {
            dealer.DeckDraw(drawFrom, drawTo, drawAmount);
            target.RemoveCoin(dealer, ReactiveCoin, threshold);
        }
    }
}