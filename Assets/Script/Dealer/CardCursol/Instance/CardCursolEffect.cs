using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardCursolEffect : ICardCursolEvent
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] float zPos;

    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        Vector3 drugPos = Camera.main.ScreenToWorldPoint(pos);
        sprite.transform.position = new Vector3(drugPos.x, drugPos.y, zPos);
    }
    public void Open(ICardPrintable card)
    {

    }
    public void Close(ICardPrintable card)
    {

    }
}
