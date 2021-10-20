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
    protected override void Skill(CardFacade dealer, Card target, Coin c, short n)
    {
        if (target.coins.ContainsKey(ReactiveCoin) && target.coins[ReactiveCoin] >= threshold)
        {
            dealer.DeckDraw(drawFrom, drawTo, drawAmount);
            target.RemoveCoin(dealer, ReactiveCoin, threshold);
        }
    }

    public override string Text()
    {
        return ReactiveCoin.name + "が" + threshold.ToString() + "枚を超えた時、" + StageDeckMethod.ToCardText(drawFrom) +
        "から" + StageDeckMethod.ToCardText(drawFrom) + "枚引く。\nその後、" +
        ReactiveCoin.name + "を" + threshold.ToString() + "枚このカードから取り除く。";
    }
}