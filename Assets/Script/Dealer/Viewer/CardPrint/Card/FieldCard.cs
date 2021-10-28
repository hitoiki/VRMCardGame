using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
#pragma warning disable 0649
[SerializeField]
public class FieldCard : MonoBehaviour, ICardPrintable, ICursolable, ICardObservable, ICardCursolEventUser
{
    //field上のカードPrefabに付けるクラス
    private ReactiveProperty<IDealableCard> card = new ReactiveProperty<IDealableCard>();
    [SerializeField] private SpriteRenderer spriteRenderer;
    public List<ICardCursolEvent> cursolEvent = new List<ICardCursolEvent>();
    public CoinCard coinCard;
    private bool activate;

    public void Print(IDealableCard c)
    {
        activate = true;
        this.card.Value = c;
        spriteRenderer.sprite = c?.GetCard().mainData.iconSprite;
        if (coinCard != null) coinCard.Print(c);
        c.SetTransform(this.transform);

    }
    public void UnPrint()
    {
        activate = false;
        coinCard.UnPrint();
    }
    public void Active(bool b)
    {
        this.gameObject.SetActive(b);
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public IDealableCard GetDealableCard()
    {
        return card.Value;
    }

    public IReadOnlyReactiveProperty<IDealableCard> ObservableCard()
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
