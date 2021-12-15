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
        SkillPack skillPack = cardViewable.GetCard().GetSkillPack();
        Debug.Log("Check");
        dealer.PrintedCardPlay(skillPack.UseSkill(), cardViewable, stage.DeckKey(DeckType.hands));
    }
}
