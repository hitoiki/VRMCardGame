using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPrintedCard : MonoBehaviour, ICardPrinted
{
    //Hand,持ち札として動かせるカード
    //KeyPadで動かしたい
    //取り敢えず何も考えず、ドラッグ出来るものを作ろう
    private Card printingCard;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void Print(Card card)
    {
        printingCard = card;
    }

    public void Active(bool b)
    {
        this.gameObject.SetActive(b);
    }
}
