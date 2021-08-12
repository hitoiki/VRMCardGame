﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UniRx;
#pragma warning disable 0649
public class HandViewer : MonoBehaviour, IGameState
{
    //手札を上手い事表示するやつ
    //fieldViewerに対して、上手くインスタンス化も挟みたい

    [SerializeField] private CardField cardField;
    [SerializeField] private GameObject initVRM = null;
    [SerializeField] private HandCard handCard = null;
    private ICardPrinted vrmPrinted = null;
    private List<ICardPrinted> printedList = new List<ICardPrinted>();
    private ObjectFlyer<HandCard> flyer;
    [SerializeField] Grid grid;
    private IDisposable _fieldPrint;


    private void OnValidate()
    {
        if ((initVRM != null) && initVRM.GetComponent<ICardPrinted>() == null) initVRM = null;

    }

    private void Start()
    {
        //InitHandがICardPrintedである事が前提条件なアレ
        if (initVRM != null) vrmPrinted = initVRM.GetComponent<ICardPrinted>();
        if (handCard != null) flyer = new ObjectFlyer<HandCard>(handCard);
    }

    //Start
    public void CrankIn()
    {
        cardField.hands.ObservableCards.Subscribe(x =>
        {
            DeckCheck(cardField.hands.cards);

        });
        DeckCheck(cardField.hands.cards);
    }
    //Update
    public void StateUpdate()
    {

    }
    //End
    public void CrankUp()
    {
        Debug.Log("CrankUp");
        //購読停止
        _fieldPrint.Dispose();
    }

    private void DeckCheck(List<Card> c)
    {
        if (c != null)
        {
            foreach (var i in c.Select((Card card, int index) => new { card, index }))
            {
                //いなかったら生成
                //格子状にしたいなら適当なmodを挟めばヨロシ

                if (i.index < printedList.Count())
                {
                    printedList[i.index].Print(i.card);
                }
                else
                {
                    ICardPrinted printedObj = flyer.GetMob(grid.Point(i.index, 0), x => { x.vrmPrinted = vrmPrinted; x.anchor = grid.Point(i.index, 0); }).GetComponent<ICardPrinted>();
                    printedList.Add(printedObj);
                    printedObj.Print(i.card);

                }

            }
        }
    }
}
