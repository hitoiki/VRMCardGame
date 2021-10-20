using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "CardText/SelectDamege")]
public class SelectDamege : ScriptableSelectSkill
{
    [SerializeField] private Coin c;
    [SerializeField] private short damegeAmo;

    protected override void Skill(CardFacade dealer, Card source, List<Card> target)
    {
        target[0].AddCoin(dealer, c, damegeAmo);
    }
    public override (StageDeck, sbyte) SelectCard(GamePlayData data, Card source)
    {
        return (StageDeck.field, 1);
    }
    public override string Text()
    {
        return "場のカード1枚に" + c.name + "を" + damegeAmo.ToString() + "枚与える";
    }
}
