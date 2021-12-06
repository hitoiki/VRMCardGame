using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            new SkillDealableCard(c, StageDeck.field, data.stage.queueObject).ChangeCoin(data.coinToCost, source.GetCard().cost);
        };
    }

    //SkillQueueの操作を伴う操作

    //カードを引いて、適当な場所に移動
    public void DeckDraw(StageDeck from, StageDeck to, int amount)
    {
        List<ICard> drawCards = data.stage.DeckKey(from).Draw(amount);
        data.stage.DeckKey(to).Add(drawCards);
        foreach (ICard card in drawCards)
        {
            //  data.skillQueue.Push(card.GetSkillPack().DrawSkill(from, to), card, null);
        }
    }

    //条件を満たすカードのリストを渡す
    public List<SkillDealableCard> FieldDeck()
    {
        List<SkillDealableCard> fieldDeck = new List<SkillDealableCard>();
        foreach (ICardPrintable c in data.fieldFactory.GetCards())
        {
            fieldDeck.Add(new SkillDealableCard(c, StageDeck.field, data.stage.queueObject));
        };
        return fieldDeck;
    }
    //Playerに対する効果
    //Damege
    public void PlayerDamage(int damage)
    {
        data.player.Damage(damage);
    }

    //カードを移動させる効果
    public void CardMove(List<SkillDealableCard> cards, StageDeck to)
    {
        /*
        foreach (SkillDealableCard cardCard in cards)
         {
             data.stage.DeckKey(cardCard.deck).Remove(cardCard);
             SkillTarget decksTarget = SkillTarget.SourceOnly(targetPrint[i], targetBelongDeck[i]);
             data.skillQueue.Push(target[i].GetSkillPack().DrawSkill(targetBelongDeck[i], to, DeckMove.Exit), decksTarget);
             data.stage.DeckKey(to).Add(target[i]);
         }*/
        Debug.Log("準備中");
    }

    public void AddCard(ICard card, StageDeck deck)
    {
        data.stage.DeckKey(deck).Add(card);
    }

}

