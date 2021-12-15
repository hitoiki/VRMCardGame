using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public Subject<SkillDealableCard> CardSelect(DeckType deck)
    {
        prepareSubject = new Subject<SkillDealableCard>();
        aimingDeck = deck;
        state.ChangeState(selectingState);
        return prepareSubject;
    }

    public void CursolCheck(ICardPrintable card, DeckType deck, ContactMode mode)
    {
        if (mode == ContactMode.Enter && deck == aimingDeck)
        {
            prepareSubject.OnNext(new SkillDealableCard(card, stage.DeckKey(deck), stage.queueObject));
            prepareSubject.OnCompleted();
            state.ChangeState(playingState);
        }
    }
}
