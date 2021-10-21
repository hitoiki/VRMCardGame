using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayChecker : MonoBehaviour
{
    [SerializeField] private StateDealer state;
    [SerializeField] private CardPlayDealer dealer = null;
    [SerializeField] private CardSelectCursolEvent cardSelect;
    [SerializeField] private GamePlayData data;
    [SerializeField] private string selectingState;

    public void CardCheck(Card card)
    {
        if (!card.IsPlayable(data))
        {
            Debug.Log("IsNotPlayable");
            return;
        }

        if (card.IsSelect(data))
        {
            cardSelect.selectingCard = card;
            Debug.Log(cardSelect.selectingCard.mainData.name);
            state.ChangeState(selectingState);
        }
        else
        {
            dealer.CardPlay(card.UseSkill());
        }
    }


}
