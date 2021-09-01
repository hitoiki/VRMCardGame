using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable 0649
[SerializeField]
public class FieldCard : MonoBehaviour, ICardPrinted
{
    //field上のカードPrefabに付けるクラス
    private Card printingCard;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void Print(Card card)
    {
        printingCard = card;

        spriteRenderer.sprite = card.mainData.iconSprite;

    }
    public void Active(bool b)
    {
        this.gameObject.SetActive(b);
    }

}
