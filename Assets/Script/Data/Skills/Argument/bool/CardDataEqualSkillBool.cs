using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataEqualSkillBool : ISkillCardBool
{
    [SerializeField] private CardData data;
    public bool SkillBool(IPermanent dealableCard)
    {
        return dealableCard.GetCardData() == data;
    }
    public string Text()
    {
        return "そのカードが" + data.textName + "であるならば";
    }

    public string SkillName()
    {
        return "";
    }
}
