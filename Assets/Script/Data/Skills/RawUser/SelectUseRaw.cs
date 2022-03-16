using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using System.Linq;

public class SelectUseRaw : ISkillProcessUse
{
    //一つカードを選択して、それを対象にRawSkillを発動する
    [SerializeReference, SubclassSelector] IRawSkill rawSkill;
    [SerializeField] private List<SelectTape> selectTapes;
    [SerializeField] SkillUsingObjectAddress address;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            IObservable<IPermanent> selected = address.selector.CardListSelect(selectTapes.Select(x => { return (x.deck, x.cardCondition); }).ToList());
            Subject<Unit> selectSubject = new Subject<Unit>();
            List<IPermanent> selectedList = new List<IPermanent>();
            selected.Subscribe(
                selectedList.Add,
            () =>
            {
                IObservable<Unit> skillSubject = Observable.Empty<Unit>();
                foreach (IPermanent x in selectedList)
                {
                    skillSubject = skillSubject.Concat(rawSkill.GetSkillProcess(facade.NewFacade(x)));
                }
                skillSubject.Subscribe(x => { }, () => { selectSubject.OnCompleted(); });
            }
            );
            return selectSubject;
        });

    }
    public bool GetIsSkillable(CardFacade facade)
    {

        foreach (var tape in selectTapes)
        {
            if (tape.cardCondition == null)
            {
                if (facade.DeckKey(tape.deck).Any()) continue;
                else return false;
            }
            if (!facade.DeckKey(tape.deck).Any(x => { return tape.cardCondition.SkillBool(x); })) return false;
        }
        return true;

    }
    public string Text()
    {
        if (selectTapes.Count == 1) return "カードを1枚選ぶ。それは" + rawSkill.Text();
        return "カードを" + selectTapes.Count.ToString() + "枚選ぶ。それらは" + rawSkill.Text();
    }

    public string SkillName()
    {
        return "SelectUse" + rawSkill.SkillName();
    }

    [System.Serializable]
    private class SelectTape
    {
        [SerializeReference, SubclassSelector] public ISkillCardBool cardCondition;
        [SerializeField] public DeckType deck;
    }
}