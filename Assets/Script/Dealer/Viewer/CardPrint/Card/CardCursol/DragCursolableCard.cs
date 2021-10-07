using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCursolableCard : MonoBehaviour, ICursolableCard
{
    Vector3 anchor;
    [SerializeField] CardPlayRecepter recepter;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Enter)
        {
            anchor = card.GetTransform().position;
        }

        if (mode == ContactMode.Exit)
        {
            recepter.CardPlayRecept(card.GetTransform().position, card.GetCard());
            card.GetTransform().position = anchor;
        }
    }
    public void CardCursol(ICardPrintable card, Vector3 pos)
    {

    }
}
