using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPrinter : SupervisedObject
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Text nameText;
    [SerializeField] private Text effectText;
    void Print(Card card)
    {
        nameText.text = card.cardName;

    }
}
