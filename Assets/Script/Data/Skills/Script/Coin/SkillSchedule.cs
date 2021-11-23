using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
[System.Serializable]
public class SkillSchedule : ICoinProcess
{
    [SerializeReference, SubclassSelector] public IRawSkill skill;
    [SerializeField] public int cycle;
    public int elapsedTime;
    [SerializeField] private Coin ReactiveCoin;

    public void GetSkillProcess(CardFacade facade, Coin c, int n)
    {
        Debug.Log("Processing:" + c.coinName + n.ToString());
        elapsedTime += n;
        Debug.Log(elapsedTime.ToString());
        while (elapsedTime >= cycle)
        {
            skill.GetSkillProcess(facade);
            elapsedTime -= cycle;
        }
    }


    public bool GetIsSkillable(CardFacade facade, Coin coin, int n)
    {
        Debug.Log("SkillableCheck");
        return ReactiveCoin == coin;
    }

    public string Text()
    {
        return ReactiveCoin.coinName + " " + elapsedTime.ToString() + "/" + cycle.ToString() + ":" + skill.Text();
    }
    public string SkillName()
    {
        return "Scheduled[" + cycle.ToString() + "," + skill.SkillName() + "]";
    }
}