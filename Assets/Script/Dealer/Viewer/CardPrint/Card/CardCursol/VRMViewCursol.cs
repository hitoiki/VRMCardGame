using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMViewCursol : MonoBehaviour, ICursolableCard
{
    public VRMPrintCard vrmPrintCard;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
    public void CardCursol(ICardPrintable card, Vector3 pos)
    {
        if (card == null) Debug.Log("noCard!");
        else vrmPrintCard.Print(card.GetCard());

    }
}
