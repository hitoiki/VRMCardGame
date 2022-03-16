using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ActionLock : ISkillProcessUse
{
    [SerializeReference, SubclassSelector] IRawSkill rawSkill;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            facade.actionTimes -= 1;
            facade.skillTarget.MoveDeck(facade.DeckKey(DeckType.discard));
            return Observable.Empty<Unit>();
        });
    }
    public bool GetIsSkillable(CardFacade facade)
    {
        return facade.actionTimes >= 1;
    }
    public string Text()
    {
        return "";
    }

    public string SkillName()
    {
        return "";
    }
}
