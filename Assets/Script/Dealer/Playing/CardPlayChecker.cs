using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UniRx;

public class CardPlayChecker : MonoBehaviour
{
    //Cardを色々勘案して、適宜処置を加える
    //今は無用の長物になってるので後々消す
    [SerializeField] private CardPlayDealer dealer = null;
    [SerializeField] private Stage stage;
    public void CardCheck(ICardPrintable cardViewable)
    {
        SkillPack skillPack = cardViewable.GetCard().GetSkillPack();
        dealer.PrintedCardPlay(skillPack.UseSkill(), cardViewable.GetCard());
    }
}
