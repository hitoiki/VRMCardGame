using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

#pragma warning disable 0649
public class VRMPrintCard : MonoBehaviour, ICardPrintable, ICardObservable
{
    // VRMのカードをやる
    private ReactiveProperty<ICard> card = new ReactiveProperty<ICard>();
    [SerializeField] private Image BackImage;
    [SerializeField] private Image FrontImage;
    [SerializeField] private Text nameText;
    private Vector3 anchor;
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
    public Transform GetTransform()
    {
        return this.transform;
    }

    public ICard GetCard()
    {
        return card.Value;
    }
    public void Active(bool boo)
    {

    }
    public void SetAnchor(Vector3 vec)
    {
        anchor = vec;
    }

    public Vector3 GetAnchor()
    {
        return anchor;
    }
}