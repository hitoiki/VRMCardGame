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
    public ICardPrintable vrmPrinted;
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

    public IReadOnlyReactiveProperty<Card> ObservableCard()
    {
        return card;
    }

    public void Click(Vector3 pos, ContactMode mode)
    {

    }
    public void Cursol(Vector3 pos)
    {
        if (activate)
        {
            if (card.Value == null) Debug.Log("noCard!");
            else vrmPrinted.Print(card.Value);
        }
    }

}
