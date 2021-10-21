using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSelectCursolEvent : MonoBehaviour, ICardCursolEvent
{
    //これが場のカード分呼び出されている可能性がちょっとある
    [SerializeField] CardPlayDealer dealer;
    public Card selectingCard;
    [SerializeField] private StateDealer state;
    [SerializeField] private string selectingState;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Enter)
        {
            List<Card> selectedCards = new List<Card>();
            selectedCards.Add(card.GetCard());

            dealer.CardPlay(
                selectingCard.UseSkill()
                .Concat(selectingCard.SelectSkill(selectedCards))
                .ToList()
            );
            selectingCard = null;
            state.ChangeState(selectingState);
        }
    }
    public void CardCursol(ICardPrintable card, Vector3 pos)
    {

    }
}
