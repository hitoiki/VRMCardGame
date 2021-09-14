using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

#pragma warning disable 0649
public class VRMPrintCard : MonoBehaviour, ICardPrinted, ICardObservable
{
    // VRMのカードをやる
    private ReactiveProperty<Card> card = new ReactiveProperty<Card>();
    [SerializeField] private Image BackImage;
    [SerializeField] private Image FrontImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text effectText;
    public void Print(Card card)
    {
        this.card.Value = card;
        nameText.text = card.mainData.cardName;
        //effectText.text = card.mainData.text;
        effectText.text = card.CardText();
        BackImage.sprite = card.mainData.backSprite;
        FrontImage.sprite = card.mainData.frontSprite;
    }

    public ReactiveProperty<Card> ObservableCard()
    {
        return card;
    }

    public void Active(bool boo)
    {

    }
}
