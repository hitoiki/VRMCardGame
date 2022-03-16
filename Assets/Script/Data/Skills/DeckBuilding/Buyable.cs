using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class Buyable : ISkillProcessKind
{
    [SerializeReference, SubclassSelector] ISkillEffect[] buyEffect;
    [SerializeReference, SubclassSelector] ISkillEffect[] unBuyEffect;
    public IObservable<Unit> GetSkillProcess(CardFacade facade, OtherSkillKind timing)
    {
        return Observable.Defer<Unit>(() =>
        {
            if (timing != OtherSkillKind.Click) return Observable.Empty<Unit>();
            if (facade.instantMoney < facade.skillTarget.GetCardData().cost) return facade.skillsSubject.EffectLoad(unBuyEffect, facade.skillTarget);
            return facade.skillsSubject.EffectLoad(buyEffect, facade.skillTarget)
                            .Concat(Observable.Defer<Unit>(() =>
                                {
                                    facade.instantMoney -= facade.skillTarget.GetCardData().cost;
                                    facade.skillTarget.MoveDeck(facade.DeckKey(DeckType.discard));
                                    return Observable.Empty<Unit>();
                                })
            );
        });

    }

    public bool GetIsSkillable(CardFacade facade, OtherSkillKind timing)
    {
        return timing == OtherSkillKind.Click;
    }

    public string Text()
    {
        return "";
    }
    public string SkillName()
    {
        return "購入";
    }
}


