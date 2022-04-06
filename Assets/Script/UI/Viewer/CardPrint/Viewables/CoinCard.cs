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
    [SerializeField] AlignGrid grid;
    [SerializeField] Vector3 origin = new Vector3(100, 0, 0);
    private List<(Coin, CoinSprite)> sprites = new List<(Coin, CoinSprite)>();
    private IDisposable _change;
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
        if (_change != null) _change.Dispose();
        foreach ((Coin coin, CoinSprite sprite) c in sprites)
        {
            c.sprite.UnPrint();
        }
    }

    public void Print(IPermanent c)
    {
        ICoinObservable coinObservable = c as ICoinObservable;
        if (coinObservable == null)
        {
            CoinInit(c.GetCoin());
            Debug.Log("Can'tObserveCoins");
            return;
        }
        _change = coinObservable.GetObservableCoin().Subscribe(changeCoin =>
        {
            if (sprites.Any(x => { return x.Item1 == changeCoin.key; }))
            {
                sprites.Where(x => { return x.Item1 == changeCoin.key; }).First().Item2.CoinPrint(changeCoin.key, changeCoin.result);
                if (changeCoin.result < 0) sprites.Where(x => { return x.Item1 == changeCoin.key; }).First().Item2.gameObject.SetActive(false);
            }
            else CoinMake(changeCoin.key, changeCoin.result);
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
