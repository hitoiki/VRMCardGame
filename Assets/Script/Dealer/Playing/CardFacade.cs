using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardFacade : MonoBehaviour
{
    //Cardが出来る処理を書く   
    //CardのSkill群にこいつが渡される
    [SerializeField] private PlayerData player;
    [SerializeField] private Stage stage = null;
    [SerializeField] private Coin coinToCost;
    [SerializeField] public SkillQueueObject skillQueue;

    public virtual void CostPay(IDealableCard card)
    {
        foreach (Card c in stage.field.cards)
        {
            c.AddCoin(this, coinToCost, card.GetCard().mainData.cost);
        };
    }

    //SkillQueueの操作を伴う操作

    //カードを引いて、適当な場所に移動
    public void DeckDraw(StageDeck from, StageDeck to, int amount)
    {
        List<IDealableCard> drawCards = stage.DeckKey(from).Draw(amount);
        stage.DeckKey(to).Add(drawCards);
        //skillQueue.Push(drawCards.SelectMany(x => { return x.DrawSkill(from, to); }));
    }

    //本当はFacadeでCoinを増減したい
    public void AddCoin()
    {

    }


    //指定されたカードデータを指定されたカードに敷く
    public void cardPut(IDealableCard cardTop, CardData cardBottom)
    {
        cardTop.GetCard().underCards.Add(cardBottom);
    }
    //指定されたカードデータを指定されたカードの下から取り除く
    public void cardUnPut(IDealableCard cardTop, CardData cardBottom)
    {
        cardTop.GetCard().underCards.Remove(cardBottom);
    }
    //条件を満たすカードのリストを渡す
    public List<IDealableCard> DeckFilter(StageDeck f, System.Func<IDealableCard, bool> ch)
    {
        return stage.DeckKey(f).cards.Where(ch).ToList();
    }
    public List<CardData> UnderCardFilter(IDealableCard c, System.Func<CardData, bool> ch)
    {
        return c.GetCard().underCards.Where(ch).ToList();
    }
    //あるカードにコインを渡す
    public void CoinToCard(IDealableCard card, Coin coin, short i)
    {
        card.GetCard().AddCoin(this, coin, i);
    }

    //あるデッキ全てにCoinを渡す
    public void CoinToDeck(StageDeck f, Coin coin, short i)
    {
        foreach (IDealableCard c in stage.DeckKey(f).cards)
        {
            c.GetCard().AddCoin(this, coin, i);
        };
    }

}

