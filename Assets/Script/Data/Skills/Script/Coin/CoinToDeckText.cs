using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
//ここに効果を書き続ける

[System.Serializable]
public class CoinToDeckText : IUseSkill
{

    [SerializeField] private Coin c;
    [SerializeField] private short amount = 0;
    [SerializeField] private StageDeck deck;
    private void Skill(CardFacade facade)
    {
        facade.CoinToDeck(deck, c, amount);
        Debug.Log("CoinToDeck");
    }
    public SkillProcess UseSkill()
    {
        return new SkillProcess(
        (CardFacade dealer) => { Skill(dealer); }
        );
    }
    public bool UseAble(Stage data)
    {
        return true;
    }
    public ICardChecking SelectCard(Stage data)
    {
        return null;
    }

    public string Text()
    {
        return StageDeckMethod.ToCardText(deck) + "の全てのカードに" + c.name + "を" + amount.ToString() + "枚与える。";
    }
}
