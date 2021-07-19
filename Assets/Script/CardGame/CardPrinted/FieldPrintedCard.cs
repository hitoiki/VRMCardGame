using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable 0649

public class FieldPrintedCard : MonoBehaviour, ICardPrinted
{
    //field上のカードPrefabに付けるクラス
    [SerializeField] private GameObject prefab;
    private Card printingCard;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Text nameText;
    [SerializeField] private Text effectText;
    public void Print(Card card)
    {
        printingCard = card;
        nameText.text = card.data.cardName;
        effectText.text = card.data.text;

    }
}
