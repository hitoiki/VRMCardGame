using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class CardSelector : MonoBehaviour
{
    // カードを選択して返すObject

    public Subject<SkillDealableCard> prepareSubject;
    private DeckType aimingDeck;

    [SerializeField] private StateDealer state;
    [SerializeField] private string selectingState;
    [SerializeField] private string playingState;
    [SerializeField] private Stage stage;

    public IObservable<SkillDealableCard> CardSelect(DeckType deck)
    {
        return Observable.Defer<SkillDealableCard>(() =>
        {
            aimingDeck = deck;
            state.ChangeState(selectingState);
            prepareSubject = new Subject<SkillDealableCard>();
            return prepareSubject;
        });
    }

    public void CursolCheck(ICardPrintable card, DeckType deck, ContactMode mode)
    {
        if (mode == ContactMode.Enter && deck == aimingDeck)
        {
            //nullSubjectを扱う可能性に注意
            prepareSubject.OnNext(new SkillDealableCard(card, stage.DeckKey(deck), stage.queueObject));
            prepareSubject.OnCompleted();
            state.ChangeState(playingState);
        }
    }
}
