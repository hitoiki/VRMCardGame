using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
#pragma warning disable 0649
[SerializeField]
public class FieldCard : MonoBehaviour, ICardPrinted, ICursolable, ICardObservable
{
    //field上のカードPrefabに付けるクラス
    private ReactiveProperty<Card> card = new ReactiveProperty<Card>();
    [SerializeField] private SpriteRenderer spriteRenderer;
    public ICardPrinted vrmPrinted;
    public void Print(Card c)
    {
        this.card.Value = c;
        spriteRenderer.sprite = c?.mainData.iconSprite;
    }
    public void Active(bool b)
    {
        this.gameObject.SetActive(b);
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
        vrmPrinted.Print(card.Value);
    }

}
