using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillTextPrinter : MonoBehaviour, ICardViewable
{
    //スッと出てくる
    [SerializeField] private Text SkillText;
    [SerializeField] private float displayTime;
    [SerializeField] private float easingTime;
    [SerializeField] private Vector2 displayPoint;
    [SerializeField] private Vector2 anchorPoint;
    [SerializeField] private RectTransform position;
    public void Print(IPermanent card)
    {
        SkillText.text = card.GetCardData().CardText();
        this.position.DOAnchorPos(displayPoint, easingTime);
        this.position.DOAnchorPos(anchorPoint, easingTime).SetDelay(displayTime);
    }

    public void UnPrint()
    {
        SkillText.text = "";
    }

    public void Active(bool b)
    {

    }
}
