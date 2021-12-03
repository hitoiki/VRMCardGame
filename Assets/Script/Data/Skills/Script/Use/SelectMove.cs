using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMove : IUseProcess
{
    [SerializeField] StageDeck from;
    [SerializeField] StageDeck to;
    public void GetSkillProcess(CardFacade facade)
    {
        facade.CardMove(facade.target, to);
    }

    public bool GetIsSkillable(CardFacade facade)
    {
        return true;
    }
    public ICardChecking PlayPrepare()
    {
        return new SelectDeckCardChecking(from);
    }
    public string Text()
    {
        return StageDeckMethod.ToCardText(from) + "のカード1枚を" + StageDeckMethod.ToCardText(to) + "へ送る";
    }

    public string SkillName()
    {
        return "SelectMove:" + StageDeckMethod.ToCardText(from) + "," + StageDeckMethod.ToCardText(from);
    }
}