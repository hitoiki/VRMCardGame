using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoinSkill
{
    [SerializeReference, SubclassSelector] public ICoinProcess coinSkill;
    [SerializeReference, SubclassSelector] public ISkillEffect[] effect;
    public CoinSkill(ICoinProcess CoinSkill, ISkillEffect[] Effect)
    {
        this.effect = Effect;
        this.coinSkill = CoinSkill;
    }

    public Skill GetSkill(Coin c, int n)
    {
        return new Skill(coinSkill.SkillName(), effect, x => coinSkill.GetSkillProcess(x, c, n), x => coinSkill.GetIsSkillable(x, c, n));
    }
}