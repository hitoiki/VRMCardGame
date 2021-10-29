using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultDealableCard : IDealableCard
{
    // デフォルトで扱うICardDealable
    // 基本的にこれらを扱うが、いつの日か別のクラスを用いる日も来るだろう
    private CoinSet coinSet;
    private Card card;
    private Transform transform;

    public DefaultDealableCard(Card Card, Transform Transform)
    {
        this.card = Card;
        this.transform = Transform;
        coinSet = new CoinSet();
    }

    public Card GetCard()
    {
        return card;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public CoinSet GetCoin()
    {
        return coinSet;
    }

    public void SetCard(Card Card)
    {
        this.card = Card;
    }
    public void SetTransform(Transform Transform)
    {
        this.transform = Transform;
    }
}
