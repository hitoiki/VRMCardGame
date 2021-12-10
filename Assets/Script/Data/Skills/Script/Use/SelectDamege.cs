using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SelectDamege : IUseProcess
{
    [SerializeField] private Coin c;
    [SerializeField] private short damegeAmo;
    public void GetSkillProcess(CardFacade facade)
    {

        facade.target[0].ChangeCoin(c, damegeAmo);
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
        return "場のカード1枚に" + c.name + "を" + damegeAmo.ToString() + "枚与える";
    }

    public string SkillName()
    {
        return "SelectDamege(" + c.coinName + "," + damegeAmo.ToString() + ")";
    }
}
