using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CostCard : MonoBehaviour, ICardViewable
{
    //Costを表示するためのViewable
    [SerializeField] SpriteRenderer costView;
    [SerializeField] Sprite red;
    [SerializeField] Sprite cyan;
    [SerializeField] Sprite violet;
    public void Active(bool b)
    {
        this.gameObject.SetActive(b);
    }
    public void UnPrint()
    {

    }

    public void Print(ICard c)
    {
        switch (c.GetCardData().suit)
        {
            case Suit.Red:
                costView.sprite = red;
                break;
            case Suit.Cyan:
                costView.sprite = cyan;
                break;
            case Suit.Vioret:
                costView.sprite = violet;
                break;
            default: break;
        }
    }
}
