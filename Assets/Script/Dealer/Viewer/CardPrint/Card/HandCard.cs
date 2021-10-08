using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCard : MonoBehaviour, ICardPrintable, ICursolable, ICardCursolEventUser
{
    //Hand,持ち札として動かせるカード
    //KeyPadで動かせる。また、離した場所に応じてなんかする
    private Card card;
    public Vector3 anchor = Vector3.zero;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    public List<ICardCursolEvent> cursolEvent = new List<ICardCursolEvent>();
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
    public void AddCardCursolEvent(ICardCursolEvent c)
    {
        cursolEvent.Add(c);
    }
    public void RemoveCardCursolEvent(ICardCursolEvent c)
    {
        cursolEvent.Remove(c);
    }
    public void SubstitutionCardCursolEvent(List<ICardCursolEvent> c)
    {
        cursolEvent = c;
    }
    public void Click(Vector3 pos, ContactMode mode)
    {
        if (activate)
        {
            foreach (ICardCursolEvent c in cursolEvent)
            {
                c.CardClick(this, pos, mode);
            }
        }
    }
    public void Cursol(Vector3 pos)
    {
        if (activate)
        {
            foreach (ICardCursolEvent c in cursolEvent)
            {
                c.CardCursol(this, pos);
            }
        }
    }

}
