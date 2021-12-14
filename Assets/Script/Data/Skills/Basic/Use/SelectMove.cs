using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMove : IUseProcess
{
    [SerializeField] DeckType from;
    [SerializeField] DeckType to;
    public void GetSkillProcess(CardFacade facade)
    {
        facade.MoveCard(facade.source, DeckType.field);
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