using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using System.Linq;

public class DefaultCard : ICard
{
    // デフォルトで扱うICard
    public Dictionary<Coin, int> coins = new Dictionary<Coin, int>();
    private CardData card;
    //追加効果
    private SkillPack secondSkillPack;

    public DefaultCard(CardData newCard)
    {
        this.card = newCard;
    }

    public CardData GetCardData()
    {
        return card;
    }
    public Dictionary<Coin, int> GetCoin()
    {
        return coins;
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
}
