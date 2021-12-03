using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
//ここに効果を書き続ける

[System.Serializable]
public class CoinToDeckText : IUseProcess
{

    [SerializeField] private Coin c;
    [SerializeField] private short amount = 0;
    [SerializeField] private StageDeck deck;
    public void GetSkillProcess(CardFacade facade)
    {

        foreach (SkillDealableCard card in facade.FieldDeck())
        {
            card.ChangeCoin(c, amount);
        }
        Debug.Log("CoinToDeck");
    }

    public bool GetIsSkillable(CardFacade facade)
    {
        return true;
    }

    public ICardChecking PlayPrepare()
    {
        return null;
    }

    public string Text()
    {
        return StageDeckMethod.ToCardText(deck) + "の全てのカードに" + c.name + "を" + amount.ToString() + "枚与える。";
    }

    public string SkillName()
    {
        return "DeckToCoin(" + StageDeckMethod.ToCardText(deck) + "," + c.coinName + "," + amount.ToString() + ")";
    }
}
