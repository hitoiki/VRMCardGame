using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardFacade
{
    //Cardが出来る処理を書く   
    //CardのSkill群にこいつが渡される
    //ここにメソッドを書き連ねていくのはあんま良くない気はしないでもない
    FacadeData data;
    ICardPrintable sourcePrint;
    public StageDeck sourceBelongDeck;
    IDealableCard source;
    ICardPrintable[] targetPrint;
    public StageDeck[] targetBelongDeck;
    IDealableCard[] target;
    public Dictionary<Coin, int> sourceCoins => source.GetCoin().coins;
    public CardFacade(FacadeData Data, SkillTarget skillTarget)
    {
        this.data = Data;

        sourcePrint = skillTarget.source;
        targetPrint = skillTarget.target;

        sourceBelongDeck = skillTarget.sourceBelongDeck;
        targetBelongDeck = skillTarget.targetBelongDeck;

        this.source = skillTarget.source?.GetDealableCard();
        this.target = skillTarget.target?.Select(x => { return x.GetDealableCard(); }).ToArray();
    }
    public virtual void CostPay()
    {
        foreach (ICardPrintable c in data.fieldFactory.GetCards())
        {
            c.GetDealableCard().GetCoin().ChangeCoin(data.coinToCost, source.GetCard().cost);
            SkillTarget coinsTarget = SkillTarget.SourceOnly(c, StageDeck.field);
            data.skillQueue.Push(c.GetDealableCard().GetSkillPack().CoinSkill(data.coinToCost, source.GetCard().cost), coinsTarget);
        };
    }

    //SkillQueueの操作を伴う操作

    //カードを引いて、適当な場所に移動
    public void DeckDraw(StageDeck from, StageDeck to, int amount)
    {
        List<IDealableCard> drawCards = data.stage.DeckKey(from).Draw(amount);
        data.stage.DeckKey(to).Add(drawCards);
        foreach (IDealableCard card in drawCards)
        {
            //  data.skillQueue.Push(card.GetSkillPack().DrawSkill(from, to), card, null);
        }
    }
    //CoinEffect呼びたくないときに
    private void AdjustCoin(IDealableCard card, Coin coin, int i)
    {
        card.GetCoin().ChangeCoin(coin, i);
    }

    //条件を満たすカードのリストを渡す
    public List<IDealableCard> DeckFilter(StageDeck f, System.Func<IDealableCard, bool> ch)
    {
        return data.stage.DeckKey(f).cards.Where(ch).ToList();
    }
    //あるデッキ全てにCoinを渡す
    public void CoinToDeck(StageDeck f, Coin coin, int i)
    {
        foreach (IDealableCard c in data.stage.DeckKey(f).cards)
        {
            c.GetCoin().ChangeCoin(coin, i);
        };
        if (f == StageDeck.field)
        {
            foreach (ICardPrintable c in data.fieldFactory.GetCards())
            {
                SkillTarget coinsTarget = SkillTarget.SourceOnly(c, StageDeck.field);
                data.skillQueue.Push(c.GetDealableCard().GetSkillPack().CoinSkill(coin, i), coinsTarget);
            }
        }
        if (f == StageDeck.hands)
        {
            foreach (ICardPrintable c in data.handFactory.GetCards())
            {
                SkillTarget coinsTarget = SkillTarget.SourceOnly(c, StageDeck.hands);
                data.skillQueue.Push(c.GetDealableCard().GetSkillPack().CoinSkill(coin, i), coinsTarget);
            }
        }
        if (f != StageDeck.field && f != StageDeck.hands)
        {
            Debug.Log(StageDeckMethod.ToStringFast(f) + "でのCoinEffect誘発は実装されてません");
        }

    }
    public void CoinToSource(Coin coin, int i)
    {
        source.GetCoin().ChangeCoin(coin, i);
        SkillTarget coinsTarget = SkillTarget.SourceOnly(sourcePrint, sourceBelongDeck);
        data.skillQueue.Push(source.GetSkillPack().CoinSkill(coin, i), coinsTarget);
    }

    public void CoinAdjustSource(Coin coin, int i)
    {
        AdjustCoin(source, coin, i);
    }

    public void CoinToTarget(int index, Coin coin, int i)
    {
        target[index].GetCoin().ChangeCoin(coin, i);
        SkillTarget coinsTarget = SkillTarget.SourceOnly(targetPrint[index], targetBelongDeck[index]);
        data.skillQueue.Push(target[index].GetSkillPack().CoinSkill(coin, i), coinsTarget);

    }
    //Playerに対する効果
    //Damege
    public void PlayerDamage(int damage)
    {
        data.player.Damage(damage);
    }

    //カードを移動させる効果
    public void FieldTargetMove(StageDeck deck)
    {
        data.stage.DeckKey(StageDeck.field).Remove(target.ToList());
        data.stage.DeckKey(deck).Add(target.ToList());
    }

}

