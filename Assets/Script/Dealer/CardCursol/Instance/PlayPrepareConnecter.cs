using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlayPrepareConnecter : ICardCursolEvent
{
    [SerializeField] PlayPrepareCursol prepareCursol;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        prepareCursol.CardSelect(card, pos, mode);
    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
}
