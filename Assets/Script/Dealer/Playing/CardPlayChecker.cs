using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayChecker : MonoBehaviour
{
    [SerializeField] private StateDealer state;
    [SerializeField] private CardDealer dealer;
    [SerializeField] private CardPlayRunner runner = null;
    [SerializeField] private CardSelectCursolEvent cardSelect;
    [SerializeField] private string selectingState;

    public void CardCheck(Card card)
    {
        if (card.IsPlayable(dealer))
        {
            if (card.IsSelect())
            {
                cardSelect.selectingCard = card;
                Debug.Log(cardSelect.selectingCard.mainData.name);
                state.ChangeState(selectingState);
            }
            else
            {
                Debug.Log("Run");
                runner.CardPlay(card.UseSkill());

            }
        }
    }


}
