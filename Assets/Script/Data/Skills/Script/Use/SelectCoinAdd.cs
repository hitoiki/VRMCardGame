using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SelectCoinAdd : IUseProcess
{
    [SerializeField] private Coin c;
    [SerializeField] private short Amo;
    [SerializeField] private DeckType selectDeck;
    public void GetSkillProcess(CardFacade facade)
    {
        facade.target[0].ChangeCoin(c, Amo);
    }

    public bool GetIsSkillable(CardFacade facade)
    {
        return true;
    }
    public ICardChecking PlayPrepare()
    {
        return new SelectDeckCardChecking(selectDeck);
    }
    public string Text()
    {
        return "場のカード1枚に" + c.name + "を" + Amo.ToString() + "枚与える";
    }

    public string SkillName()
    {
        return "SelectDamege(" + c.coinName + "," + Amo.ToString() + ")";
    }
}
