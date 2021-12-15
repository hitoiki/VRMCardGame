using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class SelectUseRaw : IUseProcess
{
    //一つカードを選択して、それを対象にRawSkillを発動する
    [SerializeReference, SubclassSelector] IRawSkill rawSkill;
    [SerializeField] SkillUsingObjectAddress address;
    [SerializeField] private DeckType deck;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        Subject<SkillDealableCard> selected = address.selector.CardSelect(deck);

        Subject<Unit> skillSubject = new Subject<Unit>();
        Debug.Log("aaaa");
        selected.Subscribe(x =>
        {
            rawSkill.GetSkillProcess(facade.NewFacade(x));
            Debug.Log("bbbb");
            skillSubject.OnCompleted();
        });
        return skillSubject;

    }
    public bool GetIsSkillable(CardFacade facade)
    {
        return true;
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