using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UniRx;

public class CardPlayPrepare : MonoBehaviour
{
    //ICardCursolEventを介してここに接続してもらう

    public delegate ICard selectSequence(DeckType deck);
    public Subject<ICardPrintable> prepareSubject;

    private DeckType aimingDeck;

    public void CardSelect(ICardPrintable card, DeckType deck, ContactMode mode)
    {
        if (mode == ContactMode.Enter && deck == aimingDeck)
        {
            prepareSubject.OnNext(card);
            prepareSubject.OnCompleted();
        }
    }

    public IObservable<ICardPrintable> Checking(ICardChecking checking)
    {
        prepareSubject = new Subject<ICardPrintable>();
        aimingDeck = checking.GetDeck();
        return prepareSubject;
    }
}
