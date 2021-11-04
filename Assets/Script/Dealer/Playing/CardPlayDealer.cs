using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardPlayDealer : MonoBehaviour
{
    //CardSkillをStackに入れて順次実行するクラス

    [SerializeField] private StateDealer state;
    [SerializeField] private string skillingState;
    [SerializeField] private Stage stage;
    SkillQueueObject skillQueueObject => stage.queueObject;
    [SerializeField] FacadeData facadeData;

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
            (Skill skill, SkillTarget target) runningSkill = skillQueueObject.skillQueue.Dequeue();
            Instantiate(runningSkill.skill.effect).Effect(runningSkill.target);
            runningSkill.skill.process.skill(new CardFacade(facadeData, runningSkill.target));
            SkillCount++;
            if (SkillCount > 99)
            {
                Debug.Log("OverFlow!!!!");
                break;
            }
        }
    }

    public void CardPlay(List<Skill> skills, IDealableCard Source, IDealableCard[] Target)
    {
        skillQueueObject.Push(skills, Source, Target);
        SkillExecute();
        //state.ChangeState(skillingState);
    }
}
