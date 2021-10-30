using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "CardText/GetCard")]
public class GetCard : ScriptableUseSkill
{
    [SerializeField] short getAmo = 1;
    protected override void Skill(CardFacade dealer)
    {
        dealer.DeckDraw(StageDeck.field, StageDeck.hands, getAmo);
    }
    public override bool UseAble(Stage data)
    {
        return true;
    }
    public override (StageDeck, sbyte)? SelectCard(Stage data)
    {
        return null;
    }

    public override string Text()
    {
        return "場のカードを古い方から" + getAmo.ToString() + "枚手札に加える。";
    }


}
