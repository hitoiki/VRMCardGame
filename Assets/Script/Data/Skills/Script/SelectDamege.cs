using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "CardText/SelectDamege")]
public class SelectDamege : ScriptableSelectSkill
{
    [SerializeField] private Coin c;
    [SerializeField] private short damegeAmo;

    protected override void Skill(CardDealer dealer, Card source, List<Card> target)
    {
        target[0].AddCoin(dealer, c, damegeAmo);
    }
    public override (StageDeck, sbyte)? SelectCard(CardDealer dealer, Card source)
    {
        return (StageDeck.field, 1);
    }
}
