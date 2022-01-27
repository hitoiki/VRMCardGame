using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using System.Linq;

public class DefaultCard : ICard
{
    // デフォルトで扱うICard
    // 基本的にこれらを扱うが、いつの日か別のクラスを用いる日も来るだろう
    private readonly ReactiveDictionary<Coin, int> _coins = new ReactiveDictionary<Coin, int>();
    private Subject<EffectLocation> _effectSubject => new Subject<EffectLocation>();

    //中身の値だけを公開するためのList(このListの値を変えてもReactiveCollection側は変わらない)
    public Dictionary<Coin, int> coins => _coins.ToDictionary(pair => pair.Key, pair => pair.Value);

    private CardData card;
    public IDeck onDeck { get; private set; }
    //追加効果
    private SkillPack secondSkillPack;

    public DefaultCard(CardData Card, IDeck deck)
    {
        this.card = Card;
        this.onDeck = deck;
    }

    public CardData GetCardData()
    {
        return card;
    }
    public Dictionary<Coin, int> GetCoin()
    {
        return coins;
    }
    public IReadOnlyReactiveDictionary<Coin, int> GetObserveCoin()
    {
        return _coins as IReadOnlyReactiveDictionary<Coin, int>;
    }
    public SkillPack GetSkillPack()
    {
        return SkillPack.Concat(card.skillPack, secondSkillPack);
    }

    public void SetCard(CardData Card)
    {
        this.card = Card;
    }
    public void SetSecondSkillPack(SkillPack PackSet)
    {
        this.secondSkillPack = PackSet;
    }

    public void AddSecondSkillPack(SkillPack PackSet)
    {
        SkillPack.Concat(this.secondSkillPack, PackSet);
    }

    public void ChangeCoin(Coin c, int n)
    {
        if (_coins.ContainsKey(c)) _coins[c] += n;
        //ないなら追加
        else _coins.Add(c, n);
        //負数なら削除
        if (_coins[c] < 0) _coins.Remove(c);
    }

    public void MoveDeck(IDeck deck)
    {
        onDeck.Remove(this);
        deck.Add(this);
        onDeck = deck;
    }

    public Subject<EffectLocation> EffectSubject()
    {
        return _effectSubject;
    }

    public void Dispose()
    {

    }
}
