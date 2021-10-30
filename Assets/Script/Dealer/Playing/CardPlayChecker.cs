using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPlayChecker : MonoBehaviour
{
    //Cardを色々勘案して、適宜適宜する
    [SerializeField] private StateDealer state;
    [SerializeField] private CardPlayDealer dealer = null;
    [SerializeField] private CardSelectCursolEvent cardSelect;
    [SerializeField] private Stage data;
    [SerializeField] private string selectingState;

    public void CardCheck(IDealableCard cardViewable)
    {
        Card card = cardViewable.GetCard();
        if (!card.IsPlayable(data))
        {
            Debug.Log("IsNotPlayable");
            return;
        }

        if (card.PlayPrepare(data).Any(x => { return x != null; }))
        {
            cardSelect.selectingCard = cardViewable;
            Debug.Log(cardSelect.selectingCard.GetCard().mainData.name);
            state.ChangeState(selectingState);
        }
        else
        {
            dealer.CardPlay(card.UseSkill(), cardViewable, null);
        }
    }


}
