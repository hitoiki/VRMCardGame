using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCursolScaler : ICardCursolEvent
{
    [SerializeField] private Vector3 aimingScale;
    [SerializeField] private Vector3 defaultScale;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Stay) return;
        if (mode == ContactMode.Enter) card.GetTransform().GetComponent<BoxCollider>().size = aimingScale;
        if (mode == ContactMode.Exit) card.GetTransform().GetComponent<BoxCollider>().size = defaultScale;
    }
    public void Close(ICardPrintable card, Vector3 pos)
    {
        card.GetTransform().GetComponent<BoxCollider>().size = defaultScale;
    }

}