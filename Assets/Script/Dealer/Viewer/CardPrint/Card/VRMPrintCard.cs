using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

#pragma warning disable 0649
public class VRMPrintCard : MonoBehaviour, ICardPrintable, ICardObservable
{
    // VRMのカードをやる
    private ReactiveProperty<Card> card = new ReactiveProperty<Card>();
    [SerializeField] private Image BackImage;
    [SerializeField] private Image FrontImage;
    [SerializeField] private Text nameText;
    public void Print(Card card)
    {
        this.card.Value = card;
        nameText.text = card.mainData.cardName;
        //effectText.text = card.Cardtext();
        BackImage.sprite = card.mainData.backSprite;
        FrontImage.sprite = card.mainData.frontSprite;
    }

    public void UnPrint()
    {

    }

    public IReadOnlyReactiveProperty<Card> ObservableCard()
    {
        return card;
    }
    public Transform GetTransform()
    {
        return this.transform;
    }

    public void Active(bool boo)
    {

    }
}
