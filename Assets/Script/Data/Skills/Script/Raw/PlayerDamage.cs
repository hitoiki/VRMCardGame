using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : IRawSkill
{
    [SerializeField] int damage;
    public SkillProcess GetProcess()
    {
        return x => { x.PlayerDamage(damage); };
    }

    public string Text()
    {
        return "プレイヤーに" + damage.ToString() + "ダメージ。";
    }
}
