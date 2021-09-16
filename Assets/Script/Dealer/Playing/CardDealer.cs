using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    //Cardが出来る処理を書く   
    //CardのEffect群にこいつが渡される
    [SerializeField] private EffectTextPrinter effectTextPrint = null;
    [SerializeField] private Stage stage = null;

    //カードを引いて、適当な場所に移動
    public void TextView(Card card)
    {
        effectTextPrint.Print(card);
    }
    public void DeckDraw(StageDeck from, StageDeck to, int amount)
    {
        stage.DeckKey(to).Add(stage.DeckKey(from).Draw(amount));
    }

    //指定されたカードを適当な場所に追加
    public void DeckAdd(StageDeck to, Card card)
    {
        stage.DeckKey(to).Add(card);
    }
    public void DeckAdd(StageDeck to, List<Card> card)
    {
        stage.DeckKey(to).Add(card);
    }
    //適当な場所から指定されたカードを削除
    public void DeckRemove(StageDeck to, Card card)
    {
        stage.DeckKey(to).Remove(card);
    }
    public void DeckRemove(StageDeck to, List<Card> card)
    {
        stage.DeckKey(to).Remove(card);
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

