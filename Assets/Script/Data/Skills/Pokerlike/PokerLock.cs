using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UniRx;

public class PokerLock : IUseProcess
{
    // 手札が指定した役を満たさない限りプレイをロックする
    // 使用可能条件がSkill単位で個別である事に注意
    [SerializeField] List<Suit> suitTrick;
    [SerializeReference, SubclassSelector] public IRawSkill skill;
    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            IObservable<Unit> skillObservable = skill.GetSkillProcess(facade);
            //使った後捨てる処理とかまだないのでここで書く
            facade.MoveCard(facade.source, DeckType.field);
            return skillObservable;
        });
    }
    public bool GetIsSkillable(CardFacade facade)
    {
        List<Suit> handSuits = facade.HandsDeck().Select(x => { return x.GetCardData().suit; }).ToList();
        foreach (Suit s in suitTrick)
        {
            if (!handSuits.Contains(s)) return false;
            handSuits.Remove(s);
        }
        return true;
    }
    public string Text()
    {
        return skill.Text();
    }

    public string SkillName()
    {
        return "Use" + skill.SkillName();
    }
}
