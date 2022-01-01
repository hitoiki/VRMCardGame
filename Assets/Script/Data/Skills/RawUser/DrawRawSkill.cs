using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class DrawRawSkill : IDrawProcess
{
    [SerializeReference, SubclassSelector] public IRawSkill skill;
    [SerializeField] private DeckType fromDeck;
    [SerializeField] private DeckType toDeck;
    public IObservable<Unit> GetSkillProcess(CardFacade facade, IDeck from, IDeck to)
    {
        return Observable.Defer<Unit>(() =>
        {
            IObservable<Unit> skillObservable = skill.GetSkillProcess(facade);
            return skillObservable;
        });
    }
    public bool GetIsSkillable(CardFacade facade, IDeck from, IDeck to)
    {
        return from.GetDeckType() == fromDeck && to.GetDeckType() == toDeck;
    }
    public string Text()
    {
        return "このカードが" + skill.Text();
    }

    public string SkillName()
    {
        return "Use" + skill.SkillName();
    }
}

