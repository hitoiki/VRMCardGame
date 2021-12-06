using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMViewCursol : ICardCursolEvent
{
    public VRMPrintCard vrmPrintCard;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (card == null) Debug.Log("noCard!");
        else vrmPrintCard.Print(card.GetCard());

    }
}
