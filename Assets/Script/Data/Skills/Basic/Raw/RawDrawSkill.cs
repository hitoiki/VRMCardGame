using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class RawDrawSkill : IRawSkill
{
    [SerializeField] int getAmo = 1;
    [SerializeField] DeckType from;
    [SerializeField] DeckType to;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        facade.DeckDraw(from, to, getAmo);
        return Observable.Empty<Unit>();
    }
    public string Text()
    {
        return StageDeckMethod.ToCardText(from) + "のカードを古い方から" + getAmo.ToString() + "枚" + StageDeckMethod.ToCardText(to) + "に加える。";
    }

    public string SkillName()
    {
        return "Drawfrom" + StageDeckMethod.ToCardText(from) + "to" + StageDeckMethod.ToCardText(to);
    }
}
