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
    [SerializeField] private Transform bundle;
    [SerializeField] private GameObject initFactory;
    [SerializeField] private Stage stage;
    [SerializeField] private DeckType observeDeckType;
    [SerializeField] private float tweenTime;
    [SerializeReference, SubclassSelector] public IAlignGrid grid;
    private ICardPrintableFactory factory;
    private IDisposable _Replace;
    private IDisposable _Add;
    private IDisposable _Remove;

    private void OnValidate()
    {
        if (initFactory == null || initFactory.GetComponent<ICardPrintableFactory>() == null) initFactory = null;
    }

    private void Awake()
    {
        factory = initFactory.GetComponent<ICardPrintableFactory>();
    }


    //Start
    public void CrankIn()
    {
        INoticeDeck observableDeck = stage.DeckKey(observeDeckType) as INoticeDeck;
        if (observableDeck == null)
        {
            DeckInit(stage.DeckKey(observeDeckType));
            Debug.Log("can't Stage Observeinging" + observeDeckType.ToStringFast());
        }
        //Deckに変更が起きた際、これが実行される
        _Replace = observableDeck.ReplaceEvent().Subscribe(x =>
         {
             //消して（購読を解除して）から、もう一度Print
             CardChange(x.NewValue, x.Index);

         });
        _Add = observableDeck.AddEvent().Subscribe(x =>
         {
             //Flyerから新しくICardPrintableを貰って、自分のリストに加える
             CardMake(x.Value, x.Index);
         });
        _Remove = observableDeck.RemoveEvent().Subscribe(x =>
         {
             //Flyerにカードを使われていない状態にしてもらって、リストから消す
             factory.CardEraceAt(x.Index);
             AllAlign();
         });
        DeckInit(stage.DeckKey(observeDeckType));
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
    private void CardMake(IPermanent card, int i)
    {
        ICardPrintable newCard = factory.CardMake(card, grid.NumberGrid(i));
        newCard.GetTransform().SetParent(bundle);
        Align(newCard, i);
        newCard.Print(card);
    }
    private void CardChange(IPermanent card, int i)
    {
        factory.GetCards()[i].UnPrint();
        factory.GetCards()[i].Print(card);
    }
    private void DeckInit(IEnumerable<IPermanent> c)
    {
        //デッキの初期化
        foreach (var i in c.Select((Value, Index) => new { Value, Index }))
        {
            if (i.Index < factory.GetCards().Count)
            {
                CardChange(i.Value, i.Index);
            }
            else
            {
                CardMake(i.Value, i.Index);
            }
        }

        if (factory.GetCards().Count > c.Count())
        {
            for (int i = factory.GetCards().Count - 1; i > c.Count() - 1; i--)
            {
                factory.CardEraceAt(i);
            }
        }

    }

    private void Align(ICardPrintable print, int i)
    {
        print.GetTransform().DOMove(grid.NumberGrid(i), tweenTime);
        print.SetAnchor(grid.NumberGrid(i));
    }


    [ContextMenu("Align")]
    public void AllAlign()
    {
        foreach (var p in factory.GetCards()?.Select((ICardPrintable Value, int Index) => new { Value, Index }))
        {
            Align(p.Value, p.Index);
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
