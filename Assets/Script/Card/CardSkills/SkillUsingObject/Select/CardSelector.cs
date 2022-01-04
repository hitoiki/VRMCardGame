using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class CardSelector : MonoBehaviour
{
    // カードを選択して返すObject

    private Subject<SkillDealableCard> prepareSubject;
    private DeckType aimingDeck;
    public ISkillBool cardCondition;

    [SerializeField] private StateDealer state;
    [SerializeField] private string selectingState;
    [SerializeField] private string playingState;
    [SerializeField] private Stage stage;
    [SerializeField] private CardSelectHighLighter highLighter;
    public IObservable<SkillDealableCard> CardSelect(DeckType deck, ISkillBool condition)
    {
        return Observable.Defer<SkillDealableCard>(() =>
        {
            aimingDeck = deck;
            cardCondition = condition;
            if (deck == DeckType.hands) highLighter.HandHighLight(condition);
            if (deck == DeckType.field) highLighter.FieldHighLight(condition);
            state.ChangeState(selectingState);
            prepareSubject = new Subject<SkillDealableCard>();
            return prepareSubject;
        });
    }

    public void CursolCheck(ICardPrintable card, DeckType deck, ContactMode mode)
    {
        if (mode == ContactMode.Enter && deck == aimingDeck)
        {

            if (cardCondition == null || cardCondition.SkillBool(new SkillDealableCard(card.GetCard(), stage.DeckKey(deck), stage.queueObject)))
            {
                //nullSubjectを扱う可能性に注意
                SkillDealableCard dealCard = new SkillDealableCard(card.GetCard(), stage.DeckKey(deck), stage.queueObject);
                dealCard.effectPrint = card;
                prepareSubject.OnNext(dealCard);
                prepareSubject.OnCompleted();
                highLighter.Erace();
                state.ChangeState(playingState);
            }
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
