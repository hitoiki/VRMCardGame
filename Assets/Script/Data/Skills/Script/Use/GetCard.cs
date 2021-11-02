using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GetCard : IUseSkill
{
    [SerializeField] short getAmo = 1;
    private void Skill(CardFacade dealer)
    {
        dealer.DeckDraw(StageDeck.field, StageDeck.hands, getAmo);
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
        return null;
    }

    public string Text()
    {
        return "場のカードを古い方から" + getAmo.ToString() + "枚手札に加える。";
    }


}
