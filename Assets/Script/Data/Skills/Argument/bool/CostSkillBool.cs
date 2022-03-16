using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostSkillBool : ISkillCardBool
{
    [SerializeField] private int threshold;
    [SerializeField] private ComparisonEnum equalSign;
    public bool SkillBool(IPermanent dealableCard)
    {
        return equalSign.Check(dealableCard.GetCardData().cost, threshold);
    }
    public string Text()
    {
        return equalSign.CardText("カードのコスト", threshold.ToString());
    }

    public string SkillName()
    {
        return "";
    }
}
