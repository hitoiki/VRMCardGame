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
        //暫定
        cardSelect.selectingCard = cardViewable;
        if (!card.IsPlayable(data))
        {
            Debug.Log("IsNotPlayable");
            return;
        }

        if (card.PlayPrepare(data).Any(x => { return x != null; }))
        {
            foreach (ICardChecking checking in card.PlayPrepare(data))
            {

                checking.Check(this);
            }
        }
        else
        {
            Debug.Log("Check,UnSelect");
            dealer.CardPlay(card.UseSkill(), cardViewable, null);
        }
    }
    public void SelectStageCard(StageDeck deck, sbyte amo)
    {
        Debug.Log("Check,Select" + cardSelect.selectingCard.GetCard().mainData.name);
        state.ChangeState(selectingState);
    }


}
