using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCard : MonoBehaviour, ICardPrintable, ICursolable, ICardCursolEventUser
{
    //Hand,持ち札として動かせるカード
    //KeyPadで動かせる。また、離した場所に応じてなんかする
    private IDealableCard card;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    public List<ICardCursolEvent> cursolEvent = new List<ICardCursolEvent>();
    private bool activate;
    public void Print(IDealableCard c)
    {
        activate = true;
        card = c;
        spriteRenderer.sprite = c.GetCard().mainData.iconSprite;
        c.SetTransform(this.transform);
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
    public IDealableCard GetDealableCard()
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