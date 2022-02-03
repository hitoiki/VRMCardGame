using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using System.Linq;
public class TrickUseSkill : IUseProcess
{
    //役のカードを消費する。無ければ発動出来なくなる
    [SerializeField] DeckType deck;
    [SerializeField] List<Suit> trick;
    [SerializeField] SkillUsingObjectAddress address;

    public IObservable<Unit> GetSkillProcess(CardFacade facade)
    {
        return Observable.Defer<Unit>(() =>
        {
            IObservable<ICard> selected = address.selector.CardListSelect(trick.Select(x => { return (deck, new SuitSkillBool(x) as ISkillBool); }).ToList());
            List<ICard> trash = new List<ICard>();
            Subject<Unit> skillSubject = new Subject<Unit>();
            selected.Subscribe(x =>
            {
                trash.Add(x);
            },
            () =>
            {
                foreach (ICard s in trash)
                {
                    s.MoveDeck(facade.DeckKey(DeckType.discard));
                }
                skillSubject.OnCompleted();
            }
            );
            return skillSubject;
        });


    }
    public bool GetIsSkillable(CardFacade facade)
    {
        List<Suit> handSuits = facade.DeckKey(DeckType.hands).Select(x => { return x.GetCardData().suit; }).ToList();
        foreach (Suit s in trick)
        {
            if (!handSuits.Contains(s)) return false;
            handSuits.Remove(s);
        }
        return true;
    }
    public string Text()
    {
        string str = "使用コスト:";
        foreach (Suit s in trick)
        {
            str += s.Text() + " ";
        }
        str += "\n";
        return str;
    }

    public string SkillName()
    {
        return "Juel";
    }
}