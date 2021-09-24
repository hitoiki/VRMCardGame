using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

public class CoinCard : MonoBehaviour, ICardPrintable
{
    //CardのCoinを表示するウィンドウ的な奴
    //StageCardViewerでこいつを表示
    //コイツがPrintされたとき、CoinFlyerからCoinSpriteを受け取って表示
    public ObjectFlyer<CoinSprite> flyer;
    [SerializeField] Grid grid;
    private List<CoinSprite> sprites = new List<CoinSprite>();
    private IDisposable _add;
    private IDisposable _replace;
    private IDisposable _remove;
    public void Active(bool b)
    {
        this.gameObject.SetActive(b);
    }

    public void UnPrint()
    {

    }

    public void Print(Card c)
    {
        _replace = c.coins.ObserveReplace().Subscribe(x =>
        {
            sprites.Where(y => { return y.printingCoin == x.Key; }).First().CoinPrint(x.Key, x.NewValue);
        });
        _add = c.coins.ObserveAdd().Subscribe(x =>
          {
              CoinSprite newSprite = flyer.GetMob(new Vector3(100, 0, 0));
              newSprite.gameObject.transform.SetParent(this.transform);
              sprites.Add(newSprite);
              newSprite.CoinPrint(x.Key, x.Value);
              newSprite.rect.position = grid.NumberGrid(0);
          });
        _remove = c.coins.ObserveRemove().Subscribe(x =>
         {
             sprites.Where(y => { return y.printingCoin == x.Key; }).First().gameObject.SetActive(false);
         });


    }
}
