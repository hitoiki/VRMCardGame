using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCursolableCard : MonoBehaviour, ICardCursolEvent
{
    Vector3 anchor;
    [SerializeField] CardPlayRecepter recepter;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Enter)
        {
            anchor = card.GetDealableCard().GetTransform().position;
        }
        if (mode == ContactMode.Stay)
        {
            Vector3 drugPos = Camera.main.ScreenToWorldPoint(pos);
            card.GetDealableCard().GetTransform().position = new Vector3(drugPos.x, drugPos.y, transform.position.z);
        }

        if (mode == ContactMode.Exit)
        {
            Vector3 buf = card.GetDealableCard().GetTransform().position;
            card.GetDealableCard().GetTransform().position = anchor;
            recepter.CardPlayRecept(buf, card.GetDealableCard());
        }
    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
}
