using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SelectDamege : IUseSkill
{
    [SerializeField] private Coin c;
    [SerializeField] private short damegeAmo;

    private void Skill(CardFacade facade)
    {

        facade.CoinToTarget(0, c, damegeAmo);
    }
    public SkillProcess UseSkill()
    {
        return new SkillProcess(
        (CardFacade dealer) => { Skill(dealer); }
        );
    }
    public bool UseAble(Stage data)
    {
        return true;
    }
    public (StageDeck, sbyte)? SelectCard(Stage data)
    {
        return (StageDeck.field, 1);
    }
    public string Text()
    {
        return "場のカード1枚に" + c.name + "を" + damegeAmo.ToString() + "枚与える";
    }
}
