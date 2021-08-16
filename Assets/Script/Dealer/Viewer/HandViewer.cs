using System.Collections;
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

    [SerializeField] private Stage stage;
    [SerializeField] private GameObject initVRM = null;
    [SerializeField] private HandCard handCard = null;
    private ICardPrinted vrmPrinted = null;
    private List<ICardPrinted> printedList = new List<ICardPrinted>();
    private ObjectFlyer<HandCard> flyer;
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
        if (handCard != null) flyer = new ObjectFlyer<HandCard>(handCard);
    }

    //Start
    public void CrankIn()
    {
        //Deckに変更が起きた際、これが実行される
        stage.hands.ObservableReplace.Subscribe(x =>
        {
            printedList[x.Index].Print(x.NewValue);

        });
        stage.hands.ObservableAdd.Subscribe(x =>
        {
            ICardPrinted printedObj = flyer.GetMob(grid.Point(x.Index, 0), y => { y.vrmPrinted = vrmPrinted; y.anchor = grid.Point(x.Index, 0); }).GetComponent<ICardPrinted>();
            printedList.Add(printedObj);
            printedObj.Print(x.Value);
        });
        stage.hands.ObservableRemove.Subscribe(x =>
        {
            printedList[x.Index].Active(false);
            printedList.RemoveAt(x.Index);

        });
        DeckInit(stage.hands.cards);
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

    private void DeckInit(List<Card> c)
    {
        if (c != null)
        {
            foreach (var i in c.Select((Card card, int index) => new { card, index }))
            {
                //デッキの初期化
                ICardPrinted printedObj = flyer.GetMob(grid.Point(i.index, 0), x => { x.vrmPrinted = vrmPrinted; x.anchor = grid.Point(i.index, 0); }).GetComponent<ICardPrinted>();
                printedList.Add(printedObj);
                printedObj.Print(i.card);

            }
        }
    }
}
