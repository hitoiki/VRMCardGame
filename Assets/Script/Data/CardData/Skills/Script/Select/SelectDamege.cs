using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "CardText/SelectDamege")]
public class SelectDamege : ScriptableUseSkill
{
    [SerializeField] private Coin c;
    [SerializeField] private short damegeAmo;

    protected override void Skill(CardFacade facade)
    {

        facade.CoinToTarget(0, c, damegeAmo);
    }
    public override bool UseAble(Stage data)
    {
        return true;
    }
    public override (StageDeck, sbyte)? SelectCard(Stage data)
    {
        return (StageDeck.field, 1);
    }
    public override string Text()
    {
        return "場のカード1枚に" + c.name + "を" + damegeAmo.ToString() + "枚与える";
    }
}
