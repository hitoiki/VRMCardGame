using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawDeckToCoin : IRawSkill
{
    [SerializeField] private Coin c;
    [SerializeField] private short amount = 0;
    [SerializeField] private DeckType deck;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            foreach (SkillDealableCard card in facade.FieldDeck())
            {
                card.ChangeCoin(c, amount);
            }
            return Observable.Empty<Unit>();
        });
    }

    public string Text()
    {
        return DeckTypeMethod.ToCardText(deck) + "の全てのカードに" + c.name + "を" + amount.ToString() + "枚与える。";
    }

    public string SkillName()
    {
        return "DeckToCoin(" + DeckTypeMethod.ToCardText(deck) + "," + c.coinName + "," + amount.ToString() + ")";
    }
}

