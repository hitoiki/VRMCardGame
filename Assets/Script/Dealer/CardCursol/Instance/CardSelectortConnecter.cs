using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CardSelectorConnecter : ICardCursolEvent
{
    [SerializeField] CardSelector selector;
    [SerializeField] Stage stage;
    [SerializeField] DeckType deck;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        selector.CursolCheck(card, deck, mode);
    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
}
