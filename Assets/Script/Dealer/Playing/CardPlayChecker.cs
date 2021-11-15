using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UniRx;

public class CardPlayChecker : MonoBehaviour
{
    //Cardを色々勘案して、適宜適宜する
    [SerializeField] private StateDealer state;
    [SerializeField] private string selectingState;
    [SerializeField] private string defaultState;
    [SerializeField] private Stage data;
    [SerializeField] private CardPlayDealer dealer = null;
    [SerializeField] private CardPlayPrepare prepare;
    public void CardCheck(IDealableCard cardViewable)
    {
        SkillPack checkSkillPack = cardViewable.GetSkillPack();
        if (checkSkillPack.PlayPrepare(data).Any(x => { return x != null; }))
        {
            StartCoroutine(SkillPrepare(cardViewable));
        }
        else
        {
            Debug.Log("Check,UnSelect");
            dealer.CardPlay(checkSkillPack.UseSkill(), cardViewable, null);
        }
    }
    private IEnumerator SkillPrepare(IDealableCard cardViewable)
    {
        List<IDealableCard> skillTarget = new List<IDealableCard>();
        state.ChangeState(selectingState);
        foreach (ICardChecking checkEvent in cardViewable.GetSkillPack().PlayPrepare(data))
        {
            //コルーチンで一つ一つ回していく
            IObservable<IDealableCard> preparing = prepare.Checking(checkEvent);
            preparing.Subscribe(x => { skillTarget.Add(x); });
            yield return preparing.First().ToYieldInstruction();
        }
        state.ChangeState(defaultState);
        dealer.CardPlay(cardViewable.GetSkillPack().UseSkill(), cardViewable, skillTarget.ToArray());
    }
}
