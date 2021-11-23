using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DragCursolableCard : ICardCursolEvent
{
    Vector3 anchor;
    [SerializeField] float zPos;
    [SerializeField] CardPlayRecepter recepter;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Enter)
        {
            anchor = card.GetTransform().position;
        }
        if (mode == ContactMode.Stay)
        {
            Vector3 drugPos = Camera.main.ScreenToWorldPoint(pos);
            card.GetTransform().position = new Vector3(drugPos.x, drugPos.y, zPos);
        }

        if (mode == ContactMode.Exit)
        {
            Vector3 buf = card.GetTransform().position;
            card.GetTransform().position = anchor;
            recepter.CardPlayRecept(buf, card);
        }
    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
}
