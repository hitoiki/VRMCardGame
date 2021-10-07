using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayChecker : MonoBehaviour
{
    [SerializeField] private StateDealer state;
    [SerializeField] private CardDealer dealer = null;
    [SerializeField] private string changeStateName;

    public void CardPlay(Card card)
    {
        if (card.SelectActive())
        {
            state.ChangeState(changeStateName);
        }
        else
        {
            card.UseEffect(dealer);
        }
    }


}
