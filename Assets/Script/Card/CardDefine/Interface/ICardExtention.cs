using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ICardExtention
{
    public static void BootOtherSkill(this ICard card, OtherSkillKind kind, SkillQueueObject skillQueue)
    {
        skillQueue.Push(card.GetSkillPack().OtherSkill(kind), card);
    }
}
