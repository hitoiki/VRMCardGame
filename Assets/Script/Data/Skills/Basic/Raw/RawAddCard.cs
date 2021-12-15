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
        facade.AddCard(new DefaultCard(card), to);
        return Observable.Empty<Unit>();
    }

    public string Text()
    {
        return DeckTypeMethod.ToCardText(to) + "に" + card.cardName + "を加える。";
    }

    public string SkillName()
    {
        return "AddCard:" + DeckTypeMethod.ToCardText(to) + "," + card.cardName;
    }
}