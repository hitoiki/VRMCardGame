using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawDrawSkill : IRawSkill
{
    [SerializeField] int getAmo = 1;
    [SerializeField] StageDeck from;
    [SerializeField] StageDeck to;
    public void GetSkillProcess(CardFacade facade)
    {
        facade.DeckDraw(from, to, getAmo);
    }
    public string Text()
    {
        return StageDeckMethod.ToCardText(from) + "のカードを古い方から" + getAmo.ToString() + "枚" + StageDeckMethod.ToCardText(to) + "に加える。";
    }

    public string SkillName()
    {
        return "Drawfrom" + StageDeckMethod.ToCardText(from) + "to" + StageDeckMethod.ToCardText(to);
    }
}
