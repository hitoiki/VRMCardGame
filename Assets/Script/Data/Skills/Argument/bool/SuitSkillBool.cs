using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitSkillBool : ISkillCardBool
{
    [SerializeField] private Suit boolSuit;
    public bool SkillBool(IPermanent dealableCard)
    {
        return dealableCard.GetCardData().suit == boolSuit;
    }
    public string Text()
    {
        return "スートが" + boolSuit.ToString() + "である";
    }

    public string SkillName()
    {
        return "";
    }
}
