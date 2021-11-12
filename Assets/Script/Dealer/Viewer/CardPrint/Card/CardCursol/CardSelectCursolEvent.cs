using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSelectCursolEvent : MonoBehaviour, ICardCursolEvent
{
    //これが場のカード分呼び出されている可能性がちょっとある
    [SerializeField] CardPlayDealer dealer;
    public IDealableCard selectingCard;
    [SerializeField] private StateDealer state;
    [SerializeField] private string selectingState;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Enter)
        {
            List<IDealableCard> selectedCards = new List<IDealableCard>();
            selectedCards.Add(card.GetDealableCard());
            state.ChangeState(selectingState);

            dealer.CardPlay(
                selectingCard.GetCard().UseSkill().ToList()
                , selectingCard
                , selectedCards.ToArray()
            );
            selectingCard = null;

        }
    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
}
