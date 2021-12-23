using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewSprite : MonoBehaviour, ICardViewable
{
    // Spriteにカードを表示するだけ
    [SerializeField] SpriteRenderer r;

    public void Print(ICard card)
    {
        r.sprite = card.GetCardData().iconSprite;
    }

    public void UnPrint()
    {

    }

    public void Active(bool b)
    {
        r.transform.gameObject.SetActive(b);
    }

}
