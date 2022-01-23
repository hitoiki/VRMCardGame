using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

public class CardFacade
{
    //Cardが出来る処理を書く   
    //SkillDealableの実装に伴い、主にカードの提供、ドロー処理を行うように
    FacadeData data;
    public SkillDealableCard skillTarget;
    public SkillUsingSubject skillsSubject => data.skillsSubject;
    public CardFacade(FacadeData Data, SkillDealableCard Source)
    {
        this.data = Data;
        this.skillTarget = Source;
    }
    //あんま良くない気がするけど暫定これで
    public CardFacade NewFacade(SkillDealableCard newSource)
    {
        return new CardFacade(data, newSource);
    }
    public virtual void CostPay()
    {
        foreach (ICardPrintable c in data.fieldFactory.GetCards())
        {
            SkillDealableCard dealCard = new SkillDealableCard(c.GetCard(), data.stage.DeckKey(DeckType.field), data.stage.queueObject);
            dealCard.effectPrint = c;
            dealCard.BootOtherSkill(OtherSkillKind.OnAction);
        };
    }

    //カードを引いて、適当な場所に移動
    public void DeckDraw(DeckType from, DeckType to, int amount)
    {
        List<ICard> drawCards = data.stage.DeckKey(from).Draw(amount);
        data.stage.DeckKey(to).Add(drawCards);
    }

    //条件を満たすカードのリストを渡す
    public IDeck DeckKey(DeckType type)
    {
        return data.stage.DeckKey(type);
    }
    public List<SkillDealableCard> FieldDeck()
    {
        List<SkillDealableCard> fieldDeck = new List<SkillDealableCard>();
        foreach (ICardPrintable c in data.fieldFactory.GetCards())
        {
            SkillDealableCard dealCard = new SkillDealableCard(c.GetCard(), data.stage.DeckKey(DeckType.field), data.stage.queueObject);
            dealCard.effectPrint = c;
            fieldDeck.Add(dealCard);
        };
        return fieldDeck;
    }
    public List<SkillDealableCard> HandsDeck()
    {
        List<SkillDealableCard> handDeck = new List<SkillDealableCard>();
        foreach (ICardPrintable c in data.handFactory.GetCards())
        {
            SkillDealableCard dealCard = new SkillDealableCard(c.GetCard(), data.stage.DeckKey(DeckType.field), data.stage.queueObject);
            dealCard.effectPrint = c;
            handDeck.Add(dealCard);
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
    public void AddPack(Pack pack, DeckType deck)
    {
        foreach (ICard card in pack.GetCards())
        {
            data.stage.DeckKey(deck).Add(card);
        }
    }

    public void MoveCard(SkillDealableCard skillCard, DeckType deck)
    {
        skillCard.MoveDeck(data.stage.DeckKey(deck));
    }

    public void InformPick(SkillDealableCard pickedCard)
    {
        foreach (ICardPrintable c in data.fieldFactory.GetCards())
        {
            SkillDealableCard dealCard = new SkillDealableCard(c.GetCard(), data.stage.DeckKey(DeckType.field), data.stage.queueObject);
            dealCard.effectPrint = c;
            data.stage.queueObject.Push(c.GetCard().GetSkillPack().PickingSkill(pickedCard), dealCard);
        }
    }
    public void Shuffle(DeckType deck)
    {
        data.stage.DeckKey(deck).Shuffle();
    }

}

