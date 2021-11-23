using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UniRx;

public class CardPlayPrepare : MonoBehaviour
{
    //ICardCursolEventを介してここに接続してもらう

    public delegate IDealableCard selectSequence(StageDeck deck);
    public Subject<ICardPrintable> prepareSubject;

    private StageDeck aimingDeck;

    public void CardSelect(ICardPrintable card, StageDeck deck, ContactMode mode)
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
