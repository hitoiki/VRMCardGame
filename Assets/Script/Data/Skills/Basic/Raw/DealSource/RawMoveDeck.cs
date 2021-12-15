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
        facade.MoveCard(facade.source, toDeck);
        return Observable.Empty<Unit>();
    }
    public string Text()
    {
        return "カードを" + DeckTypeMethod.ToCardText(toDeck) + "へ送る。";
    }

    public string SkillName()
    {
        return "CardMoveto" + DeckTypeMethod.ToCardText(toDeck);
    }
}
