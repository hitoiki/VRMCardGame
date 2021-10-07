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
    public List<ICursolableCard> cursolable = new List<ICursolableCard>();
    public CardPlayRecepter recepter;
    private bool activate;
    public void Print(Card c)
    {
        activate = true;
        card = c;
        spriteRenderer.sprite = c.mainData.iconSprite;
    }
    public void UnPrint()
    {

    }

    public void Active(bool b)
    {
        activate = false;
        this.gameObject.SetActive(b);
    }
    public Transform GetTransform()
    {
        return this.transform;
    }
    public Card GetCard()
    {
        return card;
    }
    public void Click(Vector3 pos, ContactMode mode)
    {
        if (activate)
        {
            foreach (ICursolableCard c in cursolable)
            {
                c.CardClick(this, pos, mode);
            }
        }
    }
    public void Cursol(Vector3 pos)
    {
        if (activate)
        {
            foreach (ICursolableCard c in cursolable)
            {
                c.CardCursol(this, pos);
            }
        }
    }

}
