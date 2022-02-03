using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardCursolEvent
{
    void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode);
    void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode);
    //State切り替わるとき用
    void Open(ICardPrintable card);
    void Close(ICardPrintable card);
}