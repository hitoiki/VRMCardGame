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
    private ReactiveProperty<ICard> card = new ReactiveProperty<ICard>();
    [SerializeField] private SpriteRenderer spriteRenderer;
    public List<ICardCursolEvent> cursolEvent = new List<ICardCursolEvent>();
    public CoinCard coinCard;
    private bool activate;
    private Vector3 anchor;

    public void Print(ICard c)
    {
        activate = true;
        this.card.Value = c;
        spriteRenderer.sprite = c?.GetCardData().iconSprite;
        if (coinCard != null) coinCard.Print(c);
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

    public ICard GetCard()
    {
        return card.Value;
    }
    public void SetAnchor(Vector3 vec)
    {
        anchor = vec;
    }

    public Vector3 GetAnchor()
    {
        return anchor;
    }

    public IReadOnlyReactiveProperty<ICard> ObservableCard()
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
