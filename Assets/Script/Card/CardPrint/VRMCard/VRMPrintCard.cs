using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
public class VRMPrintCard : MonoBehaviour, ICardPrinted
{
    // VRMのカードをやる
    private Card printingCard;
    [SerializeField] private Image BackImage;
    [SerializeField] private Image FrontImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text effectText;
    public void Print(Card card)
    {
        printingCard = card;
        nameText.text = card.data.cardName;
        effectText.text = card.data.text;
        BackImage.sprite = card.data.backSprite;
        FrontImage.sprite = card.data.frontSprite;
    }


    public void Active(bool boo)
    {

    }
}
