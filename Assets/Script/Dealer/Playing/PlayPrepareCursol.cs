using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayPrepareCursol : MonoBehaviour
{
    //ICardCursolEventを介してここに接続してもらう
    //今はカードセレクト用の応急処置として用いる

    [SerializeField] CardPlayDealer dealer;
    public IDealableCard selectingCard;
    [SerializeField] private StateDealer state;
    [SerializeField] private string selectingState;
    public void CardSelect(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Enter)
        {
            List<IDealableCard> selectedCards = new List<IDealableCard>();
            selectedCards.Add(card.GetDealableCard());
            state.ChangeState(selectingState);
            Debug.Log(selectingCard == null);
            dealer.CardPlay(
                selectingCard.GetCard().UseSkill().ToList()
                , selectingCard
                , selectedCards.ToArray()
            );
            selectingCard = null;

        }
    }
}
