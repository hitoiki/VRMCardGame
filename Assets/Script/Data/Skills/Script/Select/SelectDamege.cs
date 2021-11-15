using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SelectDamege : IUseProcess
{
    [SerializeField] private Coin c;
    [SerializeField] private short damegeAmo;

    private void Skill(CardFacade facade)
    {

        facade.CoinToTarget(0, c, damegeAmo);
    }
    public SkillProcess GetProcess()
    {
        return new SkillProcess(
        (CardFacade dealer) => { Skill(dealer); }
        );
    }
    public bool UseAble(Stage data)
    {
        return true;
    }
    public ICardChecking PlayPrepare(Stage data)
    {
        return new SelectDeckCardChecking(StageDeck.field);
    }
    public string Text()
    {
        return "場のカード1枚に" + c.name + "を" + damegeAmo.ToString() + "枚与える";
    }
}
