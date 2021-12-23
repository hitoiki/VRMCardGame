using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class VRMPrintCard : MonoBehaviour, ICardViewable, ICardObservable
{
    // VRMのカードをやる
    private ReactiveProperty<ICard> card = new ReactiveProperty<ICard>();
    [SerializeField] private Image BackImage;
    [SerializeField] private Image FrontImage;
    [SerializeField] private Text nameText;
    public void Print(ICard card)
    {
        this.card.Value = card;
        nameText.text = card.GetCardData().cardName;
        //SkillText.text = card.Cardtext();
        BackImage.sprite = card.GetCardData().backSprite;
        FrontImage.sprite = card.GetCardData().frontSprite;
    }

    public void UnPrint()
    {

    }

    public IReadOnlyReactiveProperty<ICard> ObservableCard()
    {
        return card;
    }
    public void Active(bool boo)
    {

    }
}
