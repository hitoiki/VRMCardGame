using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMViewCursol : ICardCursolEvent
{
    public VRMPrintCard vrmPrintCard;
    private bool isDraging;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Enter) isDraging = true;
        if (mode == ContactMode.Enter) isDraging = false;
    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (card == null) Debug.Log("noCard!");
        else if (!isDraging) vrmPrintCard.Print(card.GetCard());
    }
    public void Close(ICardPrintable card, Vector3 pos)
    {

    }

}
