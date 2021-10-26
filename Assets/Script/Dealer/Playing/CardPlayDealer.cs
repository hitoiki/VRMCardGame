using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardPlayDealer : MonoBehaviour
{
    //CardSkillをStackに入れて順次実行するクラス

    [SerializeField] private StateDealer state;
    [SerializeField] private string skillingState;
    [SerializeField] SkillQueueObject skillQueueObject;
    [SerializeField] CardFacade facade;

    public void SkillExecute()
    {
        if (!skillQueueObject.skillQueue.Any())
        {
            Debug.Log("nulled");
            return;
        }
        int SkillCount = 0;
        while (skillQueueObject.skillQueue.Any())
        {
            Debug.Log("skilling");
            Skill runningSkill = skillQueueObject.skillQueue.Dequeue();

            runningSkill.process.skill(facade);
            Debug.Log("skilled");
            SkillCount++;
            if (SkillCount > 99)
            {
                Debug.Log("OverFlow!!!!");
                break;
            }
        }
    }

    public void CardPlay(List<Skill> skills)
    {
        skillQueueObject.Push(skills);
        SkillExecute();
        //state.ChangeState(skillingState);
    }
}
