using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ViewingCursol : ICardCursolEvent
{
    public GameObject[] initViewable;
    private List<ICardViewable> viewables;
    private bool isDraging;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Enter) isDraging = true;
        if (mode == ContactMode.Enter) isDraging = false;
    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (card == null) return;
        if (isDraging) return;
        foreach (ICardViewable v in viewables)
        {
            v.UnPrint();
            v.Print(card.GetPermanent());
        }

    }
    public void Open(ICardPrintable card)
    {
        List<ICardViewable> newView = new List<ICardViewable>();
        foreach (GameObject g in initViewable)
        {
            ICardViewable v = g.GetComponent<ICardViewable>();
            if (v != null) newView.Add(v);
        }
        viewables = newView;
    }
    public void Close(ICardPrintable card)
    {

    }

}
