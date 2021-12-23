using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClickableCard : MonoBehaviour, ICardPrintable, ICursolable, ICardCursolEventUser
{
    // Clickする処理のところだけ括りだして親クラスとする
    [SerializeField] List<Component> initViews;
    public List<ICardViewable> viewables;
    private ICard viewingCard;
    public List<ICardCursolEvent> cursolEvent = new List<ICardCursolEvent>();
    private bool activate;
    private Vector3 anchor;

    private void OnValidate()
    {
        if (initViews != null) initViews = initViews.Where(x => { return (x == null) || (x.GetComponent<ICardViewable>() != null); }).ToList();

    }

    public void Init()
    {
        viewables = initViews.SelectMany(x => { return x.GetComponents<ICardViewable>(); }).ToList();
    }


    public virtual void Print(ICard c)
    {
        foreach (ICardViewable viewable in viewables)
        {
            viewable.Print(c);
        }
        activate = true;
        viewingCard = c;
    }
    public virtual void UnPrint()
    {
        foreach (ICardViewable viewable in viewables)
        {
            viewable.UnPrint();
        }
        viewingCard = null;
        activate = false;
    }

    public virtual void Active(bool b)
    {
        foreach (ICardViewable viewable in viewables)
        {
            viewable.Active(b);
        }
        activate = b;
        this.gameObject.SetActive(b);
    }
    public Transform GetTransform()
    {
        return this.transform;
    }
    public ICard GetCard()
    {
        return viewingCard;
    }
    public void SetAnchor(Vector3 vec)
    {
        anchor = vec;
    }

    public Vector3 GetAnchor()
    {
        return anchor;
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
