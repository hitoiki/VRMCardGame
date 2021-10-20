using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardDealer : MonoBehaviour
{
    //CardSkillをStackに入れて順次実行するクラス

    List<CardSkill> skillList = new List<CardSkill>();
    CardFacade facade;

    public void SkillStacking(List<CardSkill> skill)
    {
        Debug.Log("Concat");
        skillList.Concat(skill).Where(x => { return x != null; });
    }

    public void StackRun()
    {
        if (skillList.Any())
        {
            Debug.Log("nulled");
            return;
        }
        skillList[0].skill(facade);
    }
}
