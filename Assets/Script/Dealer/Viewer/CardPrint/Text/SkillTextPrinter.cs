using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

#pragma warning disable 0649
public class SkillTextPrinter : MonoBehaviour, ICardPrintable
{
    //スッと出てくる
    [SerializeField] private Text SkillText;
    [SerializeField] private float displayTime;
    [SerializeField] private float easingTime;
    [SerializeField] private Vector2 displayPoint;
    [SerializeField] private Vector2 anchorPoint;
    [SerializeField] private RectTransform position;
    private ICard c;
    public void Print(ICard card)
    {
        c = card;
        SkillText.text = card.GetCardData().CardText();
        this.position.DOAnchorPos(displayPoint, easingTime);
        this.position.DOAnchorPos(anchorPoint, easingTime).SetDelay(displayTime);
    }
    public void UnPrint()
    {

    }
    public void Active(bool boo)
    {

    }
    public Transform GetTransform()
    {
        return this.transform;
    }
    public ICard GetDealableCard()
    {
        return c;
    }
}
