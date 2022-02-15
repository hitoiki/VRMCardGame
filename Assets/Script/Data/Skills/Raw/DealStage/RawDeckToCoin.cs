using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawDeckToCoin : IRawSkill
{
    [SerializeField] private Coin c;
    [SerializeReference, SubclassSelector] private ISkillInt number;
    [SerializeField] private DeckType deck;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            foreach (IPermanent card in facade.DeckKey(DeckType.field))
            {
                card.ChangeCoin(c, number.SkillInt(facade));
            }
            return Observable.Empty<Unit>();
        });
    }

    public string Text()
    {
        return DeckTypeMethod.ToCardText(deck) + "の全てのカードに" + c.name + "を" + number.Text() + "枚与える。";
    }

    public string SkillName()
    {
        return "DeckToCoin(" + DeckTypeMethod.ToCardText(deck) + "," + c.name + "," + number.SkillName() + ")";
    }
}


