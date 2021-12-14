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
    [SerializeField] private Stage stage;
    public void CardCheck(ICardPrintable cardViewable)
    {
        SkillPack checkSkillPack = cardViewable.GetCard().GetSkillPack();
        if (checkSkillPack.PlayPrepare().Any(x => { return x != null; }))
        {
            StartCoroutine(SkillPrepare(cardViewable));
        }
        else
        {
            Debug.Log("Check,UnSelect");
            dealer.PrintedCardPlay(checkSkillPack.UseSkill(), cardViewable, stage.DeckKey(DeckType.hands));
        }
    }
    private IEnumerator SkillPrepare(ICardPrintable cardViewable)
    {
        List<(ICardPrintable, IDeck)> skillTarget = new List<(ICardPrintable, IDeck)>();
        state.ChangeState(selectingState);
        foreach (ICardChecking checkEvent in cardViewable.GetCard().GetSkillPack().PlayPrepare())
        {
            //コルーチンで一つ一つ回していく
            IObservable<ICardPrintable> preparing = prepare.Checking(checkEvent);
            preparing.Subscribe(x => { skillTarget.Add((x, stage.DeckKey(checkEvent.GetDeck()))); });
            yield return preparing.First().ToYieldInstruction();

        }
        state.ChangeState(defaultState);
        dealer.PrintedCardPlay(cardViewable.GetCard().GetSkillPack().UseSkill(), cardViewable, stage.DeckKey(DeckType.hands), skillTarget.ToList());
    }
}
