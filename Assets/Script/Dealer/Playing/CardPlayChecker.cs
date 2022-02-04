using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UniRx;

public class CardPlayChecker : MonoBehaviour
{
    //DealerにPlay処理を渡すクラス
    [SerializeField] private CardPlayDealer dealer = null;
    [SerializeField] private Stage stage;
    public void CardCheck(ICardPrintable cardViewable)
    {
        SkillPack skillPack = cardViewable.GetCard().GetSkillPack();
        dealer.CardPlay(skillPack.UseSkill(), cardViewable.GetCard());
    }

    public void TurnEnd()
    {
        List<Skill> endSkill = new List<Skill>();
        endSkill.Add(StaticSkill.turnEnd);
        dealer.CardPlay(endSkill, null);
    }
}
