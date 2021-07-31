using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable 0649
[SerializeField]
public class FieldPrintedCard : MonoBehaviour, ICardPrinted
{
    //field上のカードPrefabに付けるクラス
    private Card printingCard;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Text nameText;
    [SerializeField] private Text effectText;
    public void Print(Card card)
    {
        printingCard = card;
        nameText.text = card.cardName;
        effectText.text = card.text;

    }

    public void Active(bool b)
    {
        this.gameObject.SetActive(b);
    }
}
