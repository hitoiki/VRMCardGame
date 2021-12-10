﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

public class CardFacade
{
    //Cardが出来る処理を書く   
    //SkillDealableの実装に伴い、主にカードの提供、ドロー処理を行うように
    FacadeData data;
    public SkillDealableCard source;
    public List<SkillDealableCard> target = new List<SkillDealableCard>();
    public CardFacade(FacadeData Data, SkillDealableCard Source, List<SkillDealableCard> Target)
    {
        this.data = Data;
        this.source = Source;
        this.target = Target;
    }
    public virtual void CostPay()
    {
        foreach (ICardPrintable c in data.fieldFactory.GetCards())
        {
            new SkillDealableCard(c, data.stage.DeckKey(DeckType.field), data.stage.queueObject).ChangeCoin(data.coinToCost, source.GetCard().cost);
        };
    }

    //カードを引いて、適当な場所に移動
    public void DeckDraw(DeckType from, DeckType to, int amount)
    {
        List<ICard> drawCards = data.stage.DeckKey(from).Draw(amount);
        data.stage.DeckKey(to).Add(drawCards);
        foreach (ICard card in drawCards)
        {
            // new SkillDealableCard(card, to, data.stage.queueObject);
        }
        Debug.Log("DrawSkill未対応");
    }

    //条件を満たすカードのリストを渡す
    public List<SkillDealableCard> FieldDeck()
    {
        List<SkillDealableCard> fieldDeck = new List<SkillDealableCard>();
        foreach (ICardPrintable c in data.fieldFactory.GetCards())
        {
            fieldDeck.Add(new SkillDealableCard(c, data.stage.DeckKey(DeckType.field), data.stage.queueObject));
        };
        return fieldDeck;
    }
    public List<SkillDealableCard> HandsDeck()
    {
        List<SkillDealableCard> handDeck = new List<SkillDealableCard>();
        foreach (ICardPrintable c in data.handFactory.GetCards())
        {
            handDeck.Add(new SkillDealableCard(c, data.stage.DeckKey(DeckType.field), data.stage.queueObject));
        };
        return handDeck;
    }
    //Playerに対する効果
    //Damege
    public void PlayerDamage(int damage)
    {
        data.player.Damage(damage);
    }

    public void AddCard(ICard card, DeckType deck)
    {
        data.stage.DeckKey(deck).Add(card);
    }

    public void MoveCard(SkillDealableCard skillCard, DeckType deck)
    {
        skillCard.MoveDeck(data.stage.DeckKey(deck));
    }

}

