using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
[System.Serializable]
public class SkillSchedule : ICoinProcess
{
    [SerializeReference, SubclassSelector] public IRawSkill skill;
    [SerializeField] public int cycle;
    [HideInInspector] public int elapsedTime;
    [SerializeField] private Coin ReactiveCoin;

    private SkillProcess GetSkillProcess(Coin c, int n)
    {
        SkillProcess process = StaticSkills.IdentitySkill.process;
        elapsedTime += n;
        while (elapsedTime >= cycle)
        {
            process += skill.GetProcess();
            elapsedTime -= cycle;
        }
        return process;
    }
    public SkillProcess GetProcess(Coin coin, int n)
    {
        return new SkillProcess(GetSkillProcess(coin, n));
    }

    public IsSkillable GetIsSkillable(Coin coin, int n)
    {
        return facade => { return ReactiveCoin == coin && facade.sourceCoins[ReactiveCoin] >= cycle; };
    }

    public string Text()
    {
        return ReactiveCoin.coinName + " " + elapsedTime.ToString() + "/" + cycle.ToString() + ":" + skill.Text();
    }
}