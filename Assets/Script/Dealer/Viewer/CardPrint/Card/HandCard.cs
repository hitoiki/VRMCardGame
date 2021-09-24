using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCard : MonoBehaviour, ICardPrintable, ICursolable
{
    //Hand,持ち札として動かせるカード
    //KeyPadで動かせる。また、離した場所に応じてなんかする
    private Card card;
    public Vector3 anchor = Vector3.zero;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    public ICardPrintable vrmPrinted;
    public CardPlayRecepter recepter;
    public void Print(Card c)
    {
        card = c;
        spriteRenderer.sprite = c.mainData.iconSprite;
    }
    public void UnPrint()
    {

    }

    public void Active(bool b)
    {
        this.gameObject.SetActive(b);
    }
    public void Click(Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Exit)
        {
            recepter.CardPlayRecept(this.transform.position, this.card);
            this.transform.position = anchor;
        }
    }
    public void Cursol(Vector3 pos)
    {
        vrmPrinted.Print(card);
    }
}
