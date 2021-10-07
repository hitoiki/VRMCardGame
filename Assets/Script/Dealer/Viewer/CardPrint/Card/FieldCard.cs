using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
#pragma warning disable 0649
[SerializeField]
public class FieldCard : MonoBehaviour, ICardPrintable, ICursolable, ICardObservable
{
    //field上のカードPrefabに付けるクラス
    private ReactiveProperty<Card> card = new ReactiveProperty<Card>();
    [SerializeField] private SpriteRenderer spriteRenderer;
    public List<ICursolableCard> cursolable = new List<ICursolableCard>();
    public CoinCard coinCard;
    private bool activate;

    public void Print(Card c)
    {
        activate = true;
        this.card.Value = c;
        spriteRenderer.sprite = c?.mainData.iconSprite;
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

    public Card GetCard()
    {
        return card.Value;
    }

    public IReadOnlyReactiveProperty<Card> ObservableCard()
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
