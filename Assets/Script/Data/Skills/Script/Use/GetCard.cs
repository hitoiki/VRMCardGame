using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GetCard : IUseProcess
{
    [SerializeField] int getAmo = 1;
    public void GetSkillProcess(CardFacade facade)
    {
        foreach (SkillDealableCard s in facade.target)
        {
            facade.MoveCard(s, DeckType.hands);
        }
    }
    public bool GetIsSkillable(CardFacade facade)
    {
        return true;
    }
    public ICardChecking PlayPrepare()
    {
        return new SelectDeckCardChecking(DeckType.field);
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
