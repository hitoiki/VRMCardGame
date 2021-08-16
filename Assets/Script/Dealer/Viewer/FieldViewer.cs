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
    [SerializeField] private FieldCard fieldCard = null;
    private List<ICardPrinted> printedList = new List<ICardPrinted>();
    private ObjectFlyer<FieldCard> flyer;
    [SerializeField] Grid grid;
    private IDisposable _FieldReplace;
    private IDisposable _FieldAdd;
    private IDisposable _FieldRemove;
    private void Start()
    {
        //InitHandがICardPrintedである事が前提条件なアレ
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
            ICardPrinted printedObj = flyer.GetMob(grid.Point(x.Index, 0), y => { }).GetComponent<ICardPrinted>();
            printedList.Add(printedObj);
            printedObj.Print(x.Value);
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
        _FieldReplace.Dispose();
        _FieldAdd.Dispose();
        _FieldRemove.Dispose();
    }

    private void DeckInit(List<Card> c)
    {
        if (c != null)
        {
            foreach (var i in c.Select((Card card, int index) => new { card, index }))
            {
                //デッキの初期化
                ICardPrinted printedObj = flyer.GetMob(grid.Point(i.index, 0), x => { }).GetComponent<ICardPrinted>();
                printedList.Add(printedObj);
                printedObj.Print(i.card);

            }
        }
    }
}
