using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UniRx;
#pragma warning disable 0649
public class FieldViewer : MonoBehaviour, IGameState
{

    //手札を上手い事表示するやつ
    //fieldViewerに対して、上手くインスタンス化も挟みたい

    [SerializeField] private Stage stage;
    [SerializeField] private GameObject initVRM = null;
    [SerializeField] private FieldCard fieldCard = null;
    private ICardPrinted vrmPrinted = null;
    private List<ICardPrinted> printedList = new List<ICardPrinted>();
    private ObjectFlyer<FieldCard> flyer;
    [SerializeField] Grid grid;
    private IDisposable _HandReplace;
    private IDisposable _HandAdd;
    private IDisposable _HandRemove;


    private void OnValidate()
    {
        if ((initVRM != null) && initVRM.GetComponent<ICardPrinted>() == null) initVRM = null;

    }

    private void Start()
    {
        //InitHandがICardPrintedである事が前提条件なアレ
        if (initVRM != null) vrmPrinted = initVRM.GetComponent<ICardPrinted>();
        if (fieldCard != null) flyer = new ObjectFlyer<FieldCard>(fieldCard);
    }

    //Start
    public void CrankIn()
    {
        //Deckに変更が起きた際、これが実行される
        stage.field.ObservableReplace.Subscribe(x =>
        {
            printedList[x.Index].Print(x.NewValue);

        });
        stage.field.ObservableAdd.Subscribe(x =>
        {
            CardMake(x.Value, x.Index);
        });
        stage.field.ObservableRemove.Subscribe(x =>
        {
            printedList[x.Index].Active(false);
            printedList.RemoveAt(x.Index);

        });
        DeckInit(stage.field.cards);
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
        _HandReplace.Dispose();
        _HandAdd.Dispose();
        _HandRemove.Dispose();
    }

    private void CardMake(Card card, int index)
    {
        ICardPrinted printedObj = flyer.GetMob(grid.Point(index, 0), y => { y.vrmPrinted = vrmPrinted; }).GetComponent<ICardPrinted>();
        printedList.Add(printedObj);
        printedObj.Print(card);
    }

    private void DeckInit(List<Card> c)
    {
        if (c != null)
        {
            foreach (var x in c.Select((Card card, int index) => new { card, index }))
            {
                //デッキの初期化
                CardMake(x.card, x.index);

            }
        }
    }
}
