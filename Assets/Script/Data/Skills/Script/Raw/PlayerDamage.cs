using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : IRawSkill
{
    [SerializeField] int damage;
    public void GetSkillProcess(CardFacade facade)
    {
        facade.PlayerDamage(damage);
    }

    public string Text()
    {
        return "プレイヤーに" + damage.ToString() + "ダメージ。";
    }

    public string SkillName()
    {
        return "PlayerDamage:" + damage.ToString();
    }
}
