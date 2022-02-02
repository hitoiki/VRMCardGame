using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class ClickableCard : MonoBehaviour, ICardPrintable, ICardCursolEventUser
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

    //ICardPrintable用
    public virtual void Print(ICard c)
    {
        foreach (ICardViewable viewable in viewables)
        {
            viewable.Print(c);
        }
        activate = true;
        c.GetEffectProjector().EffectSubScribe(this);
        viewingCard = c;
    }
    public virtual void UnPrint()
    {
        foreach (ICardViewable viewable in viewables)
        {
            viewable.UnPrint();
        }
        activate = false;
        viewingCard.GetEffectProjector().EffectUnSubScribe(this);
        viewingCard = null;
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
    //EventUser用
    public void AddCardCursolEvent(ICardCursolEvent c)
    {
        cursolEvent.Add(c);
    }
    public void RemoveCardCursolEvent(ICardCursolEvent c)
    {
        c.Close(this, Vector3.zero);
        cursolEvent.Remove(c);
    }
    public void SubstitutionCardCursolEvent(List<ICardCursolEvent> c)
    {
        foreach (ICardCursolEvent cEvent in cursolEvent.Except(c))
        {
            cEvent.Close(this, Vector3.zero);
        }
        cursolEvent = c;
    }
    //Monobehaviorの機能でクリックを検知
    void OnMouseDown()
    {
        if (activate)
        {
            foreach (ICardCursolEvent c in cursolEvent)
            {
                c.CardClick(this, Input.mousePosition, ContactMode.Enter);
            }
        }
    }

    // マウスボタンを離した時にコールされる
    void OnMouseUp()
    {
        if (activate)
        {
            foreach (ICardCursolEvent c in cursolEvent)
            {
                c.CardClick(this, Input.mousePosition, ContactMode.Exit);
            }
        }
    }

    // マウスボタンが押された状態でマウスを移動させてる間コールされ続ける
    void OnMouseDrag()
    {
        if (activate)
        {
            foreach (ICardCursolEvent c in cursolEvent)
            {
                c.CardClick(this, Input.mousePosition, ContactMode.Stay);
            }
        }
    }

    // マウスカーソルが対象オブジェクトから退出した時にコールされる
    void OnMouseExit()
    {
        if (activate)
        {
            foreach (ICardCursolEvent c in cursolEvent)
            {
                c.CardCursol(this, Input.mousePosition, ContactMode.Exit);
            }
        }
    }
    // マウスカーソルが対象オブジェクトに進入した時にコールされる
    void OnMouseEnter()
    {
        if (activate)
        {
            foreach (ICardCursolEvent c in cursolEvent)
            {
                c.CardCursol(this, Input.mousePosition, ContactMode.Enter);
            }
        }
    }

    // マウスカーソルが対象オブジェクトに重なっている間コールされ続ける
    void OnMouseOver()
    {
        if (activate)
        {
            foreach (ICardCursolEvent c in cursolEvent)
            {
                c.CardCursol(this, Input.mousePosition, ContactMode.Stay);
            }
        }
    }

}
