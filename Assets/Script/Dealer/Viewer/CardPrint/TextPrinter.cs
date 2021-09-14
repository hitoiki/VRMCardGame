using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

#pragma warning disable 0649
public class TextPrinter : MonoBehaviour, ICardPrinted
{
    //スッと出てくる
    [SerializeField] private Text effectText;
    [SerializeField] private float displayTime;
    [SerializeField] private float easingTime;
    [SerializeField] private Vector2 displayPoint;
    [SerializeField] private Vector2 anchorPoint;
    [SerializeField] private RectTransform position;
    public void Print(Card card)
    {
        effectText.text = card.CardText();
        this.position.DOAnchorPos(displayPoint, easingTime);
        this.position.DOAnchorPos(anchorPoint, easingTime).SetDelay(displayTime);
    }
    public void Active(bool boo)
    {

    }
}
