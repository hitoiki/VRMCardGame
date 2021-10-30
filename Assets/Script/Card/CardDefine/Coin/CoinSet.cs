using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

public class CoinSet
{
    //Cardに乗っているコインを示す

    private readonly ReactiveDictionary<Coin, int> _coins = new ReactiveDictionary<Coin, int>();

    //中身の値だけを公開するためのList(このListの値を変えてもReactiveCollection側は変わらない)
    public Dictionary<Coin, int> coins => _coins.ToDictionary(pair => pair.Key, pair => pair.Value);
    //ReactiveCollectionのうちIObservableだけを公開し、処理を登録できるように
    public IObservable<DictionaryReplaceEvent<Coin, int>> CoinsReplace => _coins.ObserveReplace();
    public IObservable<DictionaryAddEvent<Coin, int>> CoinsAdd => _coins.ObserveAdd();
    public IObservable<DictionaryRemoveEvent<Coin, int>> CoinsRemove => _coins.ObserveRemove();

    public CoinSet()
    {
        _coins = new ReactiveDictionary<Coin, int>();
    }
    public void ChangeCoin(Coin c, int n)
    {
        if (_coins.ContainsKey(c)) _coins[c] += n;
        //ないなら追加
        else _coins.Add(c, n);
        //負数なら削除
        if (_coins[c] < 0) _coins.Remove(c);
        Debug.Log(_coins[c]);
    }
}
