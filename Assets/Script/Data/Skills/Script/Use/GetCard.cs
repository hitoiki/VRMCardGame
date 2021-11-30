using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GetCard : IUseProcess
{
    [SerializeField] int getAmo = 1;
    public void GetSkillProcess(CardFacade facade)
    {
        facade.FieldTargetMove(StageDeck.hands);
    }
    public bool GetIsSkillable(CardFacade facade)
    {
        return true;
    }
    public ICardChecking PlayPrepare()
    {
        return new SelectDeckCardChecking(StageDeck.field);
    }

    public string Text()
    {
        return "場のカードを古い方から" + getAmo.ToString() + "枚手札に加える。";
    }

    public string SkillName()
    {
        return "GetCard";
    }

}
