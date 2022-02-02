using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

public class CardFacade
{
    //Cardが出来る処理を書く   
    //主にICardの取得処理を担当する
    FacadeData data;
    public ICard skillTarget;
    public SkillUsingSubject skillsSubject => data.skillsSubject;
    public SkillQueueObject skillQueue => data.stage.queueObject;
    public CardFacade(FacadeData Data, ICard Source)
    {
        this.data = Data;
        this.skillTarget = Source;
    }
    //あんま良くない気がするけど暫定これで
    public CardFacade NewFacade(ICard newSource)
    {
        return new CardFacade(data, newSource);
    }
    public virtual void CostPay()
    {
        foreach (ICard dealCard in data.fieldFactory.GetCards().Select(x => { return x.GetCard(); }))
        {
            dealCard.BootOtherSkill(OtherSkillKind.OnAction, skillQueue);
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
    public List<ICard> FieldDeck()
    {
        List<ICard> fieldDeck = new List<ICard>();
        foreach (ICard dealCard in data.fieldFactory.GetCards().Select(x => { return x.GetCard(); }))
        {
            //   dealCard.effectPrint = c;
            fieldDeck.Add(dealCard);
        };
        return fieldDeck;
    }
    public List<ICard> HandsDeck()
    {
        List<ICard> handDeck = new List<ICard>();
        foreach (ICard dealCard in data.handFactory.GetCards().Select(x => { return x.GetCard(); }))
        {
            // dealCard.effectPrint = c;
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

    public void MoveCard(ICard skillCard, DeckType deck)
    {
        skillCard.MoveDeck(data.stage.DeckKey(deck));
    }

    public void InformPick(ICard pickedCard)
    {
        foreach (ICard dealCard in data.fieldFactory.GetCards().Select(x => { return x.GetCard(); }))
        {
            // dealCard.effectPrint = c;
            data.stage.queueObject.Push(dealCard.GetSkillPack().PickingSkill(pickedCard), dealCard);
        }
    }
    public void Shuffle(DeckType deck)
    {
        data.stage.DeckKey(deck).Shuffle();
    }

}

