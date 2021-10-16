using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayChecker : MonoBehaviour
{
    [SerializeField] private StateDealer state;
    [SerializeField] private CardPlayRunner runner = null;
    [SerializeField] private CardSelectCursolEvent cardSelect;
    [SerializeField] private string selectingState;

    public void CardCheck(Card card)
    {
        /*
        if (card.SelectSkill() != null)
        {
            cardSelect.selectingCard = card;
            state.ChangeState(selectingState);
        }
        else
        {
            runner.CardPlay(card.UseSkill(SkillPriority.beforeCoin));
            runner.CardPlay(card.UseSkill(SkillPriority.afterCoin));

        }*/
    }


}
