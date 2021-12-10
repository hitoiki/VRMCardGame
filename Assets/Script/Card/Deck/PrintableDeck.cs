using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

public class PrintableDeck
{
    Deck deck;
    ICardFactory factory;
    Vector3 origin = Vector3.zero;

    //購読通知用
    private Subject<CollectionReplaceEvent<ICardPrintable>> subjectReplace = new Subject<CollectionReplaceEvent<ICardPrintable>>();
    private Subject<CollectionAddEvent<ICardPrintable>> subjectAdd = new Subject<CollectionAddEvent<ICardPrintable>>();
    private Subject<CollectionRemoveEvent<ICardPrintable>> subjectReMove = new Subject<CollectionRemoveEvent<ICardPrintable>>();

    public PrintableDeck(Deck Deck, ICardFactory Factory)
    {
        this.deck = Deck;
        this.factory = Factory;
    }
    public void Substitution(List<ICard> c)
    {
        deck.Substitution(c);
    }
    public void Add(List<ICard> cs)
    {
        deck.Add(cs);
    }
    public List<ICardPrintable> Pick(List<ICard> cs)
    {
        List<ICard> returnCards = deck.Pick(cs);
        return returnCards.Select(x => { return factory.CardMake(x, origin); }).ToList();
    }
    public bool ExistCheck(ICard c)
    {
        return deck.ExistCheck(c);
    }
    public List<ICard> DrawCheck(int i)
    {
        return deck.DrawCheck(i);
    }
    public List<ICardPrintable> Draw(int n)
    {
        List<ICard> returnCards = deck.Draw(n);
        return returnCards.Select(x => { return factory.CardMake(x, origin); }).ToList();
    }

    //購読用
    public ISubject<CollectionReplaceEvent<ICardPrintable>> ObservableReplace()
    {
        return subjectReplace;
    }
    public ISubject<CollectionAddEvent<ICardPrintable>> ObservableAdd()
    {
        return subjectAdd;
    }
    public ISubject<CollectionRemoveEvent<ICardPrintable>> ObservableRemove()
    {
        return subjectReMove;
    }

}
