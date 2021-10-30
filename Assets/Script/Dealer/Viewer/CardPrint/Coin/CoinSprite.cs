using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSprite : MonoBehaviour
{
    //Coin1種類を表示する単位
    public RectTransform rect;
    public Coin printingCoin;
    [SerializeField] private Image image;
    [SerializeField] private Text text;

    public void CoinPrint(Coin c, int s)
    {
        printingCoin = c;
        image.sprite = c.icon;
        text.text = s.ToString();
    }

}
