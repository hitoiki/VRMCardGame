using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayChecker : MonoBehaviour
{
    [SerializeField] private StateDealer state;
    [SerializeField] private CardDealer dealer = null;
    [SerializeField] private string selectingState;
    [SerializeField] private string effectingState;


    public void CardPlay(Card card)
    {
        if (card.SelectActive())
        {
            state.ChangeState(selectingState);
        }
        else
        {
            card.UseEffect(dealer);
        }
    }

    public void CardSelecting(Card card)
    {
        state.ChangeState(effectingState);
        card.SelectEffect(dealer, card);
        card.UseEffect(dealer);
    }


}
