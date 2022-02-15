using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitView : MonoBehaviour, ICardViewable
{
    [SerializeField] SpriteRenderer spRen;
    [SerializeField] Sprite redSuit;
    [SerializeField] Sprite cyanSuit;
    [SerializeField] Sprite violetSuit;
    [SerializeField] Sprite whiteSuit;

    public void Print(IPermanent card)
    {
        if (card.GetCardData().suit == Suit.Red) spRen.sprite = redSuit;
        if (card.GetCardData().suit == Suit.Cyan) spRen.sprite = cyanSuit;
        if (card.GetCardData().suit == Suit.Violet) spRen.sprite = violetSuit;
        if (card.GetCardData().suit == Suit.White) spRen.sprite = whiteSuit;
    }

    public void UnPrint()
    {
        spRen.sprite = null;
    }

    public void Active(bool boo)
    {
        this.gameObject.SetActive(boo);
    }
}
