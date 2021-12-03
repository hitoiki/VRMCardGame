using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class DefaultDealableCard : IDealableCard
{
    // デフォルトで扱うICardDealable
    // 基本的にこれらを扱うが、いつの日か別のクラスを用いる日も来るだろう
    private readonly ReactiveDictionary<Coin, int> _coins = new ReactiveDictionary<Coin, int>();

    //中身の値だけを公開するためのList(このListの値を変えてもReactiveCollection側は変わらない)
    public Dictionary<Coin, int> coins => _coins.ToDictionary(pair => pair.Key, pair => pair.Value);

    private Card card;
    //追加効果
    private SkillPack secondSkillPack;

    public DefaultDealableCard(Card Card)
    {
        this.card = Card;
    }

    public Card GetCard()
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

    public void SetCard(Card Card)
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
}
