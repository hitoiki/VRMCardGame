using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardCursolEvent
{
    void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode);
    void CardCursol(ICardPrintable card, Vector3 pos);
}