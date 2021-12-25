using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawMoveDeck : IRawSkill
{
    [SerializeField] DeckType toDeck;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            facade.MoveCard(facade.source, toDeck);
            return Observable.Empty<Unit>();
        });
    }
    public string Text()
    {
        return DeckTypeMethod.ToCardText(toDeck) + "へ移る。";
    }

    public string SkillName()
    {
        return "CardMoveto" + DeckTypeMethod.ToCardText(toDeck);
    }
}