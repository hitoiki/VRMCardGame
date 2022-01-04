using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using System.Linq;

public class SelectUseRaw : IUseProcess
{
    //一つカードを選択して、それを対象にRawSkillを発動する
    [SerializeReference, SubclassSelector] IRawSkill rawSkill;
    [SerializeReference, SubclassSelector] ISkillBool cardCondition;
    [SerializeField] private DeckType deck;
    [SerializeField] SkillUsingObjectAddress address;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            IObservable<SkillDealableCard> selected = address.selector.CardSelect(deck, cardCondition);

            Subject<Unit> skillSubject = new Subject<Unit>();
            selected.Subscribe(x =>
            {
                rawSkill.GetSkillProcess(facade.NewFacade(x)).Subscribe(x => { }, () => { skillSubject.OnCompleted(); });
            },
            () => { skillSubject.OnCompleted(); }
            );
            return skillSubject;
        });

    }
    public bool GetIsSkillable(CardFacade facade)
    {
        return facade.DeckKey(deck).cards.Any();
    }
    public string Text()
    {
        return "カードを一枚選ぶ。それは" + rawSkill.Text();
    }

    public string SkillName()
    {
        return "SelectUse" + rawSkill.SkillName();
    }
}