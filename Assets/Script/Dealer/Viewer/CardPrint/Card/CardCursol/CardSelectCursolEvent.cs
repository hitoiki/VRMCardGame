using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectCursolEvent : MonoBehaviour, ICardCursolEvent
{
    [SerializeField] CardPlayChecker checker;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Enter)
        {
            checker.CardSelecting(card.GetCard());
        }
    }
    public void CardCursol(ICardPrintable card, Vector3 pos)
    {

    }
}
