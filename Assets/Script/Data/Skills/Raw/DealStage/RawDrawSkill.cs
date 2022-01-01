using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawDrawSkill : IRawSkill
{
    [SerializeReference, SubclassSelector] private ISkillInt number;
    [SerializeField] DeckType from;
    [SerializeField] DeckType to;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
           {
               facade.DeckDraw(from, to, number.SkillInt(facade));
               return Observable.Empty<Unit>();
           });
    }
    public string Text()
    {
        return DeckTypeMethod.ToCardText(from) + "のカードを古い方から" + number.Text() + "枚" + DeckTypeMethod.ToCardText(to) + "に加える。";
    }

    public string SkillName()
    {
        return "Drawfrom" + DeckTypeMethod.ToCardText(from) + "to" + DeckTypeMethod.ToCardText(to);
    }
}
