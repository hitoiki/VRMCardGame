using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCursolEffect : MonoBehaviour, ICardCursolEvent
{
    [SerializeField] private SpriteRenderer sprite;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        Vector3 drugPos = Camera.main.ScreenToWorldPoint(pos);
        sprite.transform.position = new Vector3(drugPos.x, drugPos.y, transform.position.z);
    }
}
