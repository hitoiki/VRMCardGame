using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

public class StageCardViewer : MonoBehaviour
{
    // fieldViewerなどを抽象化する
    [SerializeField] private Stage stage;
    [SerializeField] private GameObject initCardPrinted;
    private ICardPrinted PrintCard = null;
    private List<ICardPrinted> printedList = new List<ICardPrinted>();
    private GameObjectFlyer flyer;
    [SerializeField] Grid grid;
    private IDisposable _Replace;
    private IDisposable _Add;
    private IDisposable _Remove;


    private void OnValidate()
    {
        if (initCardPrinted?.GetComponent<ICardPrinted>() == null) initCardPrinted = null;

    }

    private void Start()
    {
        PrintCard = initCardPrinted?.GetComponent<ICardPrinted>();
        if (PrintCard != null) flyer = new GameObjectFlyer(initCardPrinted);
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
        _Replace.Dispose();
        _Add.Dispose();
        _Remove.Dispose();
    }

    private void CardMake(Card card, int index)
    {
        ICardPrinted printedObj = flyer.GetMob(grid.NumberGrid(index), y => {/*ここに処理*/ }).GetComponent<ICardPrinted>();
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
