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
    [SerializeField] float zPos = 1;
    [SerializeField] Vector3 origin = new Vector3(100, 0, 0);
    private Dictionary<Coin, CoinSprite> sprites = new Dictionary<Coin, CoinSprite>();
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
    public ICard GetDealableCard()
    {
        return null;
    }

    public void UnPrint()
    {
        _replace.Dispose();
        _add.Dispose();
        _remove.Dispose();
    }

    public void Print(ICard c)
    {
        _replace = c.GetObserveCoin().ObserveReplace().Subscribe(changeCoin =>
        {
            sprites[changeCoin.Key].CoinPrint(changeCoin.Key, changeCoin.NewValue);
        });
        _add = c.GetObserveCoin().ObserveAdd().Subscribe(addCoin =>
          {
              CoinMake(addCoin.Key, addCoin.Value);
          });
        _remove = c.GetObserveCoin().ObserveRemove().Subscribe(removeCoin =>
         {
             sprites[removeCoin.Key].gameObject.SetActive(false);

         });

        CoinInit(c.GetCoin());
    }

    private void CoinInit(Dictionary<Coin, int> c)
    {
        foreach (var (coin, num) in c.Select(x => (x.Key, x.Value)))
        {
            CoinMake(coin, num);
        }

        foreach (Coin coin in c.Keys.Except(sprites.Keys))
        {
            sprites.Remove(coin);
        }
    }

    private void CoinMake(Coin c, int i)
    {
        if (!sprites.ContainsKey(c))
        {
            CoinSprite newSprite = flyer.GetMob(origin);
            newSprite.gameObject.transform.SetParent(this.transform);
            sprites.Add(c, newSprite);
            newSprite.CoinPrint(c, i);
            newSprite.rect.localScale = new Vector3(newSprite.rect.localScale.x * c.spriteScale, newSprite.rect.localScale.y * c.spriteScale, zPos);
            newSprite.rect.position = this.transform.position + c.spritePos; ;
        }
        else sprites[c].CoinPrint(c, i);
    }
}
