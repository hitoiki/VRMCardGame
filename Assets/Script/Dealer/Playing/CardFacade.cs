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

    public virtual void CostPay(Card card)
    {
        foreach (Card c in stage.field.cards)
        {
            c.AddCoin(this, coinToCost, card.mainData.cost);
        };
    }

    //SkillQueueの操作を伴う操作

    //カードを引いて、適当な場所に移動
    public void DeckDraw(StageDeck from, StageDeck to, int amount)
    {
        List<Card> drawCards = stage.DeckKey(from).Draw(amount);
        stage.DeckKey(to).Add(drawCards);
        skillQueue.Push(drawCards.SelectMany(x => { return x.DrawSkill(from, to); }).Where(x => { return x != null; }));
    }

    //本当はFacadeでCoinを増減したい
    public void AddCoin()
    {

    }


    //指定されたカードデータを指定されたカードに敷く
    public void cardPut(Card cardTop, CardData cardBottom)
    {
        cardTop.underCards.Add(cardBottom);
    }
    //指定されたカードデータを指定されたカードの下から取り除く
    public void cardUnPut(Card cardTop, CardData cardBottom)
    {
        cardTop.underCards.Remove(cardBottom);
    }
    //条件を満たすカードのリストを渡す
    public List<Card> DeckFilter(StageDeck f, System.Func<Card, bool> ch)
    {
        return stage.DeckKey(f).cards.Where(ch).ToList();
    }
    public List<CardData> UnderCardFilter(Card c, System.Func<CardData, bool> ch)
    {
        return c.underCards.Where(ch).ToList();
    }
    //あるカードにコインを渡す
    public void CoinToCard(Card card, Coin coin, short i)
    {
        card.AddCoin(this, coin, i);
    }

    //あるデッキ全てにCoinを渡す
    public void CoinToDeck(StageDeck f, Coin coin, short i)
    {
        foreach (Card c in stage.DeckKey(f).cards)
        {
            c.AddCoin(this, coin, i);
        };
    }

}

