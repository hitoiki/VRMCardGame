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
    public Card GetCard()
    {
        return null;
    }

    public void UnPrint()
    {
        _replace.Dispose();
        _add.Dispose();
        _remove.Dispose();
    }

    public void Print(Card c)
    {
        _replace = c.coins.ObserveReplace().Subscribe(changeCoin =>
        {
            foreach (CoinSprite s in sprites.Where(y => { return y.printingCoin == changeCoin.Key; }))
            {
                s.CoinPrint(changeCoin.Key, changeCoin.NewValue);
            };
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
             foreach (CoinSprite s in sprites.Where(y => { return y.printingCoin == removeCoin.Key; }))
             {
                 s.gameObject.SetActive(false);
             };
         });

        CoinInit(c.coins.ToDictionary(pair => pair.Key, pair => pair.Value));
    }

    private void CoinInit(Dictionary<Coin, short> c)
    {
        foreach (var i in c.Select((Value, Index) => new { Value, Index }))
        {
            if (i.Index < sprites.Count)
            {
                sprites[i.Index].CoinPrint(i.Value.Key, i.Value.Value);
            }
            else
            {
                CoinSprite newSprite = flyer.GetMob(new Vector3(100, 0, 0));
                newSprite.gameObject.transform.SetParent(this.transform);
                sprites.Add(newSprite);
                newSprite.CoinPrint(i.Value.Key, i.Value.Value);
                newSprite.rect.localScale = new Vector3(newSprite.rect.localScale.x * i.Value.Key.spriteScale, newSprite.rect.localScale.y * i.Value.Key.spriteScale, 1);
                newSprite.rect.position = this.transform.position + i.Value.Key.spritePos; ;
            }
        }

        if (sprites.Count > c.Count)
        {
            for (int i = sprites.Count - 1; i > c.Count - 1; i--)
            {
                sprites[i].gameObject.SetActive(false);
            }
        }
    }
}
