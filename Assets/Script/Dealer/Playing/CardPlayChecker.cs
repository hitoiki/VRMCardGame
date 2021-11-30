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
    [SerializeField] private CardPlayDealer dealer = null;
    [SerializeField] private CardPlayPrepare prepare;
    public void CardCheck(ICardPrintable cardViewable)
    {
        SkillPack checkSkillPack = cardViewable.GetDealableCard().GetSkillPack();
        if (checkSkillPack.PlayPrepare().Any(x => { return x != null; }))
        {
            StartCoroutine(SkillPrepare(cardViewable));
        }
        else
        {
            Debug.Log("Check,UnSelect");
            dealer.CardPlay(checkSkillPack.UseSkill(), SkillTarget.SourceOnly(cardViewable, StageDeck.hands));
        }
    }
    private IEnumerator SkillPrepare(ICardPrintable cardViewable)
    {
        List<ICardPrintable> skillTarget = new List<ICardPrintable>();
        List<StageDeck> selectedDeck = new List<StageDeck>();
        state.ChangeState(selectingState);
        foreach (ICardChecking checkEvent in cardViewable.GetDealableCard().GetSkillPack().PlayPrepare())
        {
            //コルーチンで一つ一つ回していく
            selectedDeck.Add(checkEvent.GetDeck());
            IObservable<ICardPrintable> preparing = prepare.Checking(checkEvent);
            preparing.Subscribe(x => { skillTarget.Add(x); });
            yield return preparing.First().ToYieldInstruction();

        }
        state.ChangeState(defaultState);
        dealer.CardPlay(cardViewable.GetDealableCard().GetSkillPack().UseSkill(), new SkillTarget(cardViewable, StageDeck.hands, skillTarget.ToArray(), selectedDeck.ToArray()));
    }
}
