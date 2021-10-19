using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "CardText/GetCard")]
public class GetCard : ScriptableUseSkill
{
    [SerializeField] short getAmo = 1;
    protected override void Skill(CardDealer dealer, Card source)
    {
        dealer.DeckDraw(StageDeck.field, StageDeck.hands, getAmo);
    }
    public override bool UseAble(CardDealer dealer, Card source)
    {
        return true;
    }

    public override string Text()
    {
        return "場のカードを古い方から" + getAmo.ToString() + "枚手札に加える。";
    }


}
