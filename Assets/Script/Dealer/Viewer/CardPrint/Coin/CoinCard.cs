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

    public Transform GetTransform()
    {
        return this.transform;
    }

    public void UnPrint()
    {

    }

    public void Print(Card c)
    {
        _replace = c.coins.ObserveReplace().Subscribe(changeCoin =>
        {
            sprites.Where(y => { return y.printingCoin == changeCoin.Key; }).First().CoinPrint(changeCoin.Key, changeCoin.NewValue);
        });
        _add = c.coins.ObserveAdd().Subscribe(addCoin =>
          {
              CoinSprite newSprite = flyer.GetMob(new Vector3(100, 0, 0));
              newSprite.gameObject.transform.SetParent(this.transform);
              sprites.Add(newSprite);
              newSprite.CoinPrint(addCoin.Key, addCoin.Value);
              newSprite.rect.localScale = new Vector3(newSprite.rect.localScale.x * addCoin.Key.spriteScale, newSprite.rect.localScale.y * addCoin.Key.spriteScale, 1);
              newSprite.rect.position = this.transform.position + addCoin.Key.spritePos;
          });
        _remove = c.coins.ObserveRemove().Subscribe(removeCoin =>
         {
             sprites.Where(y => { return y.printingCoin == removeCoin.Key; }).First().gameObject.SetActive(false);
         });


    }
}
