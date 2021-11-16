using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawDrawSkill : IRawSkill
{
    [SerializeField] int getAmo = 1;
    [SerializeField] StageDeck from;
    [SerializeField] StageDeck to;
    private void Skill(CardFacade dealer)
    {
        dealer.DeckDraw(from, to, getAmo);
    }
    public SkillProcess GetProcess()
    {
        return new SkillProcess(
        (CardFacade dealer) => { Skill(dealer); }
        );
    }
    public string Text()
    {
        return StageDeckMethod.ToCardText(from) + "のカードを古い方から" + getAmo.ToString() + "枚" + StageDeckMethod.ToCardText(to) + "に加える。";
    }
}
