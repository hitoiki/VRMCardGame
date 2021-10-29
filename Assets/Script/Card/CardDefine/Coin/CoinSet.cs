using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

public class CoinSet : MonoBehaviour
{
    //Cardに乗っているコインを示す

    private readonly ReactiveDictionary<Coin, short> _coins = new ReactiveDictionary<Coin, short>();

    //中身の値だけを公開するためのList(このListの値を変えてもReactiveCollection側は変わらない)
    public Dictionary<Coin, short> coins => _coins.ToDictionary(pair => pair.Key, pair => pair.Value);
    //ReactiveCollectionのうちIObservableだけを公開し、処理を登録できるように
    public IObservable<DictionaryReplaceEvent<Coin, short>> CoinsReplace => _coins.ObserveReplace();
    public IObservable<DictionaryAddEvent<Coin, short>> CoinsAdd => _coins.ObserveAdd();
    public IObservable<DictionaryRemoveEvent<Coin, short>> CoinsRemove => _coins.ObserveRemove();

    public CoinSet()
    {
        _coins = new ReactiveDictionary<Coin, short>();
    }
    public void AddCoin(Coin c, short n)
    {
        if (_coins.ContainsKey(c)) _coins[c] += n;
        else _coins.Add(c, n);
        Debug.Log(_coins[c]);
    }

    public void RemoveCoin(Coin c, short n)
    {
        if (_coins.ContainsKey(c))
        {
            if (_coins[c] > n) _coins[c] = 0;
            else _coins[c] -= n;
        }
    }

}
