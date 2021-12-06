using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckViewCard : MonoBehaviour, ICardPrintable, ICursolable, ICardCursolEventUser
{
    //Deckを参照する際に用いるCard
    private ICard card;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    public List<ICardCursolEvent> cursolEvent = new List<ICardCursolEvent>();
    private bool activate;
    public void Print(ICard c)
    {
        activate = true;
        card = c;
        spriteRenderer.sprite = c.GetCardData().iconSprite;
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
    public ICard GetCard()
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
    public void Cursol(Vector3 pos, ContactMode mode)
    {
        if (activate)
        {
            foreach (ICardCursolEvent c in cursolEvent)
            {
                c.CardCursol(this, pos, mode);
            }
        }
    }

}
