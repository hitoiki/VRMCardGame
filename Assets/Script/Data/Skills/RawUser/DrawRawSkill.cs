using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class DrawRawSkill : ISkillProcessDraw
{
    [SerializeReference, SubclassSelector] public IRawSkill skill;
    [SerializeField] private DeckType fromDeck;
    [SerializeField] private DeckType toDeck;
    public IObservable<Unit> GetSkillProcess(CardFacade facade, (IDeck from, IDeck to) value)
    {
        return Observable.Defer<Unit>(() =>
        {
            IObservable<Unit> skillObservable = skill.GetSkillProcess(facade);
            return skillObservable;
        });
    }
    public bool GetIsSkillable(CardFacade facade, (IDeck from, IDeck to) value)
    {
        return value.from.GetDeckType() == fromDeck && value.to.GetDeckType() == toDeck;
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

