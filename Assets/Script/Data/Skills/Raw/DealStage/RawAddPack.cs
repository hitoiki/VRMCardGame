using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawAddPack : IRawSkill
{
    [SerializeField] Pack pack;
    [SerializeField] DeckType to;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            foreach (CardData card in pack.GetCards())
            {
                facade.AddCard(new DefaultCard(card), to);
            }
            return Observable.Empty<Unit>();
        });
    }

    public string Text()
    {
        return DeckTypeMethod.ToCardText(to) + "に" + pack.name + "を加える。";
    }

    public string SkillName()
    {
        return "AddPack:" + DeckTypeMethod.ToCardText(to) + "," + pack.name;
    }
}