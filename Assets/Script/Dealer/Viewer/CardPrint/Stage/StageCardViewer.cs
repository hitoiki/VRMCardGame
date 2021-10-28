using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class StageCardViewer : MonoBehaviour, IGameState
{
    // fieldViewerなどを抽象化する
    // Deckに合わせて格子状に表示していくってだけ
    [SerializeField] private GameObject initFactory;
    [SerializeField] private Stage stage;
    [SerializeField] private StageDeck observeDeck;
    [SerializeField] private Grid grid;
    [SerializeField] private float tweenTime;
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
        _Replace = stage.DeckKey(observeDeck).ObservableReplace.Subscribe(x =>
         {
             //消して（購読を解除して）から、もう一度Print
             //printableList[x.Index].UnPrint();
             factory.GetCards()[x.Index].Print(x.NewValue);

         });
        _Add = stage.DeckKey(observeDeck).ObservableAdd.Subscribe(x =>
         {
             //Flyerから新しくICardPrintableを貰って、自分のリストに加える
             ICardPrintable newCard = factory.CardMake(x.Value, grid.NumberGrid(x.Index));
             newCard.Print(x.Value);
         });
        _Remove = stage.DeckKey(observeDeck).ObservableRemove.Subscribe(x =>
         {
             //Flyerにカードを使われていない状態にしてもらって、リストから消す
             factory.CardEraceAt(x.Index);
             PrintableAlign();
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
        //購読停止
        _Replace.Dispose();
        _Add.Dispose();
        _Remove.Dispose();
    }
    private void DeckInit(List<IDealableCard> c)
    {
        //デッキの初期化
        foreach (var i in c.Select((Value, Index) => new { Value, Index }))
        {
            if (i.Index < factory.GetCards().Count)
            {
                factory.GetCards()[i.Index].Print(i.Value);
            }
            else
            {
                ICardPrintable newCard = factory.CardMake(i.Value, grid.NumberGrid(i.Index));
                newCard.Print(i.Value);
            }
        }

        if (factory.GetCards().Count > c.Count)
        {
            for (int i = factory.GetCards().Count - 1; i > c.Count - 1; i--)
            {
                factory.CardEraceAt(i);
            }
        }

    }

    private void PrintableAlign()
    {
        foreach (var p in factory.GetCards()?.Select((ICardPrintable Value, int Index) => new { Value, Index }))
        {
            p.Value.GetDealableCard().GetTransform().DOMove(grid.NumberGrid(p.Index), tweenTime);
        }
    }

    public void CardReset()
    {
        for (int i = 0; i < factory.GetCards().Count; i++)
        {
            factory.CardEraceAt(i);
        }
    }
}
