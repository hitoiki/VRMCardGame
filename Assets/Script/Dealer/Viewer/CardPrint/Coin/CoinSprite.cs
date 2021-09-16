using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSprite : MonoBehaviour
{
    //Coin1種類を表示する単位
    [SerializeField] private SpriteRenderer r;
    [SerializeField] private Text t;
    public void CoinPrint(Coin c, short s)
    {
        r.sprite = c.icon;
        t.text = s.ToString();
    }

}
