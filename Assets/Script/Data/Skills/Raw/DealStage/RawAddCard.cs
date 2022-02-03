using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawAddCard : IRawSkill
{
    [SerializeField] CardData card;
    [SerializeField] DeckType to;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            facade.DeckKey(to).Add(new DefaultCard(card, facade.DeckKey(to)));
            return Observable.Empty<Unit>();
        });
    }

    public string Text()
    {
        return DeckTypeMethod.ToCardText(to) + "に" + card.name + "を加える。";
    }

    public string SkillName()
    {
        return "AddCard:" + DeckTypeMethod.ToCardText(to) + "," + card.name;
    }
}