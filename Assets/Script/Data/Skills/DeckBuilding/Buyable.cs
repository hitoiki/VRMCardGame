using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class Buyable : ISkillProcessKind
{
    [SerializeReference, SubclassSelector] ISkillInt skillInt;
    [SerializeReference, SubclassSelector] ISkillEffect[] unBuyEffect;
    [SerializeReference, SubclassSelector] ISkillEffect[] buyEffect;
    public IObservable<Unit> GetSkillProcess(CardFacade facade, OtherSkillKind timing)
    {
        return Observable.Defer<Unit>(() =>
        {
            if (timing == OtherSkillKind.Click) return Observable.Empty<Unit>();
            if (facade.instantMoney >= skillInt.SkillInt(facade)) return facade.skillsSubject.EffectLoad(unBuyEffect, facade.skillTarget);
            facade.instantMoney -= skillInt.SkillInt(facade);
            facade.skillTarget.MoveDeck(facade.DeckKey(DeckType.discard));
            return facade.skillsSubject.EffectLoad(buyEffect, facade.skillTarget); ;
        });

    }

    public bool GetIsSkillable(CardFacade facade, OtherSkillKind timing)
    {
        return timing == OtherSkillKind.Click;
    }

    public string Text()
    {
        return "価格:" + skillInt.Text();
    }
    public string SkillName()
    {
        return "購入";
    }
}


