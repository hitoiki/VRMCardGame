using System.Collections;
using System.Collections.Generic;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

#pragma warning disable 0649
public class UnderCardPrinter : MonoBehaviour, ICardPrintable, IGameState
{
    //Vrmに付いて下のカードの情報を知らせる奴
    [SerializeField] private VRMPrintCard vrm;
    [SerializeField] private Text effectText;
    [SerializeField] private float easingTime;
    [SerializeField] private Vector2 displayPoint;
    [SerializeField] private Vector2 anchorPoint;
    [SerializeField] private RectTransform rectTrans;
    [SerializeField] private ToggleSwitch toggleSwitch;
    private bool close;
    private IDisposable _Vrm;

    public void CrankIn()
    {
        vrm.ObservableCard().Subscribe(x =>
        {
            if (x != null) Print(x);
        });
        toggleSwitch.toggle.Subscribe(x =>
        {
            if (x)
            {
                this.rectTrans.DOAnchorPos(displayPoint, easingTime);
            }
            else
            {
                this.rectTrans.DOAnchorPos(anchorPoint, easingTime);
            };
        });
    }

    public void StateUpdate()
    {

    }
    //End
    public void CrankUp()
    {
        //購読停止
        _Vrm.Dispose();
    }

    public void Print(Card card)
    {
        effectText.text = card.CardText();
    }
    public void UnPrint()
    {

    }
    public void Active(bool boo)
    {

    }


}
