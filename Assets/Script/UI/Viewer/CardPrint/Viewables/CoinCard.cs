using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

public class CoinCard : MonoBehaviour, ICardViewable
{
    //CardのCoinを表示するウィンドウ的な奴
    //StageCardViewerでこいつを表示
    //コイツがPrintされたとき、CoinFlyerからCoinSpriteを受け取って表示
    //面倒だからやらないけどこれFlyerを適当なScriptableにした方が楽だな
    public EffectUsingObjectAddress initFlyer;
    public ObjectFlyer<CoinSprite> flyer;
    [SerializeField] Grid grid;
    [SerializeField] Vector3 origin = new Vector3(100, 0, 0);
    private List<(Coin, CoinSprite)> sprites = new List<(Coin, CoinSprite)>();
    private IDisposable _add;
    private IDisposable _replace;
    private IDisposable _remove;
    private void Awake()
    {
        if (initFlyer != null) flyer = initFlyer.flyer;
    }
    public void Active(bool b)
    {
        this.gameObject.SetActive(b);
    }

    public void UnPrint()
    {
        _replace.Dispose();
        _add.Dispose();
        _remove.Dispose();
        foreach ((Coin coin, CoinSprite sprite) c in sprites)
        {
            c.sprite.UnPrint();
        }
    }

    public void Print(ICard c)
    {
        _replace = c.GetObserveCoin().ObserveReplace().Subscribe(changeCoin =>
        {
            sprites.Where(x => { return x.Item1 == changeCoin.Key; }).First().Item2.CoinPrint(changeCoin.Key, changeCoin.NewValue);
        });
        _add = c.GetObserveCoin().ObserveAdd().Subscribe(addCoin =>
          {
              CoinMake(addCoin.Key, addCoin.Value);
          });
        _remove = c.GetObserveCoin().ObserveRemove().Subscribe(removeCoin =>
         {
             sprites.Where(x => { return x.Item1 == removeCoin.Key; }).First().Item2.gameObject.SetActive(false);

         });

        CoinInit(c.GetCoin());
    }

    private void CoinInit(Dictionary<Coin, int> c)
    {
        if (!c.Any()) return;
        foreach (var (coin, num) in c.Select(x => (x.Key, x.Value)))
        {
            CoinMake(coin, num);
        }

        foreach (Coin coin in c.Keys.Except(sprites.Select(x => { return x.Item1; })))
        {
            sprites.Remove(sprites.Where(x => { return x.Item1 == coin; }).First());
        }
    }

    private void CoinMake(Coin c, int i)
    {
        if (!sprites.Any(x => { return x.Item1 == c; }))
        {
            CoinSprite newSprite = flyer.GetMob(origin);
            newSprite.gameObject.transform.SetParent(this.transform);
            sprites.Add((c, newSprite));
            newSprite.CoinPrint(c, i);
            newSprite.rect.localScale = new Vector3(newSprite.rect.localScale.x * c.spriteScale, newSprite.rect.localScale.y * c.spriteScale, 1);
            newSprite.rect.position = this.transform.position + grid.NumberGrid(i);
        }
        else sprites.Where(x => { return x.Item1 == c; }).First().Item2.CoinPrint(c, i);
    }
}
