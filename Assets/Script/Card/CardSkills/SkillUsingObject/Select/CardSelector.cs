using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using System.Linq;

public class CardSelector : MonoBehaviour
{
    // カードを選択して返すObject

    private Subject<ICard> prepareSubject;
    private List<(DeckType deck, ISkillBool condition)> aiming = new List<(DeckType, ISkillBool)>();
    [SerializeField] private StateDealer state;
    [SerializeField] private string selectingState;
    [SerializeField] private string playingState;
    [SerializeField] private Stage stage;
    [SerializeField] private CardSelectHighLighter highLighter;
    public IObservable<ICard> CardSelect(DeckType deck, ISkillBool condition)
    {
        return Observable.Defer<ICard>(() =>
        {
            aiming = new List<(DeckType, ISkillBool)>();
            aiming.Add((deck, condition)); ;
            if (aiming.First().deck == DeckType.hands) highLighter.HandHighLight(aiming.First().condition);
            if (aiming.First().deck == DeckType.field) highLighter.FieldHighLight(aiming.First().condition);
            state.ChangeState(selectingState);
            prepareSubject = new Subject<ICard>();
            return prepareSubject;
        });
    }
    public IObservable<ICard> CardListSelect(List<(DeckType, ISkillBool)> selectList)
    {
        return Observable.Defer<ICard>(() =>
        {
            aiming = selectList;
            if (aiming.First().deck == DeckType.hands) highLighter.HandHighLight(aiming.First().condition);
            if (aiming.First().deck == DeckType.field) highLighter.FieldHighLight(aiming.First().condition);
            state.ChangeState(selectingState);
            prepareSubject = new Subject<ICard>();
            return prepareSubject;
        });
    }
    public void CursolCheck(ICardPrintable card, DeckType deck, ContactMode mode)
    {
        if (mode != ContactMode.Enter) return;
        if (aiming.First().deck != deck) return;
        if (aiming.First().condition != null && !aiming.First().condition.SkillBool(card.GetCard())) return;
        //条件に合うなら、OnNextで通知
        ICard dealCard = card.GetCard();
        // dealCard.effectPrint = card; effectLess
        prepareSubject.OnNext(dealCard);
        highLighter.Erace();
        //使った条件を削除
        aiming = aiming.Skip(1).ToList();
        if (aiming.Any())
        {
            if (aiming.First().deck == DeckType.hands) highLighter.HandHighLight(aiming.First().condition);
            if (aiming.First().deck == DeckType.field) highLighter.FieldHighLight(aiming.First().condition);
        }
        else
        {
            prepareSubject.OnCompleted();
            state.ChangeState(playingState);
        }
    }

    public void Cancel()
    {
        //nullSubjectを扱う可能性に注意
        prepareSubject.OnCompleted();
        highLighter.Erace();
        state.ChangeState(playingState);
    }

}
