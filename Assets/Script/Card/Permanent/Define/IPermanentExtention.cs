using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IPermanentExtention
{
    public static Dictionary<Coin, int> GetCoin(this IPermanent permanent)
    {
        return permanent.GetCard().GetCoin();
    }
    public static CardData GetCardData(this IPermanent permanent)
    {
        return permanent.GetCard().GetCardData();
    }
    public static SkillPack GetSkillPack(this IPermanent permanent)
    {
        return permanent.GetCard().GetSkillPack();
    }
    public static void BootOtherSkill(this IPermanent card, OtherSkillKind kind, SkillQueue skillQueue)
    {
        skillQueue.Push(card.GetSkillPack().SkillProcess<OtherSkillKind>(kind), card);
    }
}
