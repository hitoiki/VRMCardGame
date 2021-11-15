using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlayPrepareConnecter : ICardCursolEvent
{
    [SerializeField] CardPlayPrepare cardPrepare;
    [SerializeField] StageDeck deck;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        cardPrepare.CardSelect(card, deck, mode);
    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
}
