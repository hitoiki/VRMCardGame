using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPlayChecker : MonoBehaviour
{
    //Cardを色々勘案して、適宜適宜する
    [SerializeField] private StateDealer state;
    [SerializeField] private string selectingState;
    [SerializeField] private Stage data;
    [SerializeField] private CardPlayDealer dealer = null;
    [SerializeField] private PlayPrepareCursol prepareCursol;
    public void CardCheck(IDealableCard cardViewable)
    {
        SkillPack checkSkillPack = cardViewable.GetSkillPack();
        //暫定
        prepareCursol.selectingCard = cardViewable;
        if (!checkSkillPack.IsPlayable(data))
        {
            Debug.Log("IsNotPlayable");
            return;
        }

        if (checkSkillPack.PlayPrepare(data).Any(x => { return x != null; }))
        {
            foreach (ICardChecking checking in checkSkillPack.PlayPrepare(data))
            {
                //コルーチンで一つ一つ回していく形にいつかする
                checking.Check(this);
            }
        }
        else
        {
            Debug.Log("Check,UnSelect");
            dealer.CardPlay(checkSkillPack.UseSkill(), cardViewable, null);
        }
    }
    public void SelectStageCard(StageDeck deck, sbyte amo)
    {
        state.ChangeState(selectingState);
    }
}
