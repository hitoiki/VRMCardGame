using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

#pragma warning disable 0649
public class VRMPrintCard : MonoBehaviour, ICardPrintable, ICardObservable
{
    // VRMのカードをやる
    private ReactiveProperty<IDealableCard> card = new ReactiveProperty<IDealableCard>();
    [SerializeField] private Image BackImage;
    [SerializeField] private Image FrontImage;
    [SerializeField] private Text nameText;
    public void Print(IDealableCard card)
    {
        this.card.Value = card;
        nameText.text = card.GetCard().mainData.cardName;
        //SkillText.text = card.Cardtext();
        BackImage.sprite = card.GetCard().mainData.backSprite;
        FrontImage.sprite = card.GetCard().mainData.frontSprite;
    }

    public void UnPrint()
    {

    }

    public IReadOnlyReactiveProperty<IDealableCard> ObservableCard()
    {
        return card;
    }
    public Transform GetTransform()
    {
        return this.transform;
    }

    public IDealableCard GetDealableCard()
    {
        return card.Value;
    }

    public void Active(bool boo)
    {

    }
}
