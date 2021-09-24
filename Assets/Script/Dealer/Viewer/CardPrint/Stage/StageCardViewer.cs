using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

public class StageCardViewer : MonoBehaviour, IGameState
{
    // fieldViewerなどを抽象化する
    // Deckに合わせて格子状に表示していくってだけ
    // Init処理を購読したいのだが型が問題か
    [SerializeField] private GameObject initFactory;
    [SerializeField] private Stage stage;
    [SerializeField] private StageDeck observeDeck;
    [SerializeField] private Grid grid;
    private List<ICardPrintable> printableList = new List<ICardPrintable>();
    private ICardFactory factory;
    private IDisposable _Replace;
    private IDisposable _Add;
    private IDisposable _Remove;

    private void OnValidate()
    {
        if (initFactory == null || initFactory.GetComponent<ICardFactory>() == null) initFactory = null;
    }

    private void Awake()
    {
        factory = initFactory.GetComponent<ICardFactory>();
    }


    //Start
    public void CrankIn()
    {
        //Deckに変更が起きた際、これが実行される
        stage.DeckKey(observeDeck).ObservableReplace.Subscribe(x =>
        {
            //消して（購読を解除して）から、もう一度Print
            //printableList[x.Index].UnPrint();
            printableList[x.Index].Print(x.NewValue);

        });
        stage.DeckKey(observeDeck).ObservableAdd.Subscribe(x =>
        {
            //Flyerから新しくICardPrintableを貰って、自分のリストに加える
            ICardPrintable newCard = factory.CardMake(x.Value, grid.NumberGrid(x.Index));
            printableList.Add(newCard);
            newCard.Print(x.Value);
        });
        stage.DeckKey(observeDeck).ObservableRemove.Subscribe(x =>
        {
            //Flyerにカードを使われていない状態にしてもらって、リストから消す
            factory.CardErace(printableList[x.Index]);
            printableList.RemoveAt(x.Index);

        });
        DeckInit(stage.DeckKey(observeDeck).cards);
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
    private void DeckInit(List<Card> c)
    {
        foreach (var x in c?.Select((Card Value, int Index) => new { Value, Index }))
        {
            //デッキの初期化
            ICardPrintable newCard = factory.CardMake(x.Value, grid.NumberGrid(x.Index));
            printableList.Add(newCard);
            newCard.Print(x.Value);
        }
    }

    public void CardReset()
    {
        foreach (ICardPrintable p in printableList)
        {
            factory.CardErace(p);
            printableList.Remove(p);
        }
    }
}
