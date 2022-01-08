using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSprite : MonoBehaviour
{
    //Coin1種類を表示する単位
    public RectTransform rect;
    [SerializeField] private Image image;
    [SerializeField] private Text text;

    public void CoinPrint(Coin c, int s)
    {
        image.enabled = true;
        image.sprite = c.icon;
        text.text = s.ToString();
    }

    public void UnPrint()
    {
        image.enabled = false;
        text.text = null;
    }

}
