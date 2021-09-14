using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable 0649
[SerializeField]
public class FieldCard : MonoBehaviour, ICardPrinted, ICursolable
{
    //field上のカードPrefabに付けるクラス
    private Card card;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public ICardPrinted vrmPrinted;
    public void Print(Card c)
    {
        this.card = c;

        spriteRenderer.sprite = c.mainData.iconSprite;

    }
    public void Active(bool b)
    {
        this.gameObject.SetActive(b);
    }

    public void Click(Vector3 pos, ContactMode mode)
    {

    }
    public void Cursol(Vector3 pos)
    {
        vrmPrinted.Print(card);
    }

}
