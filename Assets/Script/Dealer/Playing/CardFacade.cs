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
    IDealableCard source;
    IDealableCard[] target;
    public Dictionary<Coin, int> sourceCoins => source.GetCoin().coins;
    public CardFacade(FacadeData Data, IDealableCard Source, IDealableCard[] Target)
    {
        this.data = Data;
        this.source = Source;
        this.target = Target;
    }

    public CardFacade(FacadeData Data, SkillTarget target)
    {
        this.data = Data;
        this.source = target.source;
        this.target = target.target;
    }


    public virtual void CostPay()
    {
        foreach (IDealableCard c in data.stage.field.cards)
        {
            ChangeCoin(c, data.coinToCost, source.GetCard().cost);
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
            data.skillQueue.Push(card.GetSkillPack().DrawSkill(from, to), card, null);
        }
    }

    //Coinの増減
    private void ChangeCoin(IDealableCard card, Coin coin, int i)
    {
        card.GetCoin().ChangeCoin(coin, i);
        data.skillQueue.Push(card.GetSkillPack().CoinSkill(coin, i), card, null);
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
            ChangeCoin(c, coin, i);
        };
    }
    public void CoinToSource(Coin coin, int i)
    {
        ChangeCoin(source, coin, i);
    }

    public void CoinAdjustSource(Coin coin, int i)
    {
        AdjustCoin(source, coin, i);
    }

    public void CoinToTarget(int index, Coin coin, int i)
    {
        ChangeCoin(target[index], coin, i);
    }
    //Playerに対する効果
    //Damege
    public void PlayerDamage(int damage)
    {
        data.player.Damage(damage);
    }

    //カードを移動させる効果
    public void TargetMove(StageDeck deck)
    {

    }

}

