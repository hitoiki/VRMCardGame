using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "CardText/GetCard")]
public class GetCard : UseText
{
    [SerializeField] short getAmo = 1;
    public override string Text()
    {
        return "場のカードを右端から" + getAmo.ToString() + "枚回収する。";
    }
    public override void Skill(CardDealer dealer, Card source)
    {
        dealer.DeckDraw(StageDeck.field, StageDeck.hands, getAmo);
    }


}
