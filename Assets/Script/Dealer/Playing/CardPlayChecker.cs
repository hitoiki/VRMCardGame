using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayChecker : MonoBehaviour
{
    [SerializeField] private StateDealer state;
    [SerializeField] private CardDealer dealer = null;
    [SerializeField] private string selectingState;
    [SerializeField] private string SkillingState;


    public void CardPlay(Card card)
    {
        if (card.SelectActive())
        {
            state.ChangeState(selectingState);
        }
        else
        {
            card.UseSkill(dealer);
        }
    }

    public void CardSelecting(Card card)
    {
        state.ChangeState(SkillingState);
        card.SelectSkill(dealer, card);
        card.UseSkill(dealer);
    }


}
