using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "CardText/SelectDamege")]
public class SelectDamege : ScriptableSelectSkill
{
    [SerializeField] private Coin c;
    [SerializeField] private short damegeAmo;

    protected override void Skill(CardFacade facade, List<Card> target)
    {

        facade.CoinToTarget(0, c, damegeAmo);
    }
    public override (StageDeck, sbyte) SelectCard(GamePlayData data)
    {
        return (StageDeck.field, 1);
    }
    public override string Text()
    {
        return "場のカード1枚に" + c.name + "を" + damegeAmo.ToString() + "枚与える";
    }
}
