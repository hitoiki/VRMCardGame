using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using System;
public class CardPlayDealer : MonoBehaviour
{
    //Skill,EffectをStackに入れて順次実行するクラス

    /*いつか使いそうなのでコメントアウト
    [SerializeField] private StateDealer state;
    [SerializeField] private string skillingState;
    [SerializeField] private string defaultState;*/
    [SerializeField] private Stage stage;
    SkillQueueObject skillQueueObject => stage.queueObject;
    [SerializeField] FacadeData facadeData;
    private int SkillCount;
    private bool isExecuting = false;

    //SkillをQueueでまとめて、それを処理するコルーチンをObservable化したSkillで回す
    private IEnumerator SkillExecute()
    {
        isExecuting = true;
        SkillCount = 0;
        while (skillQueueObject.Any())
        {

            //SkillをQueueから取り出し
            (Skill skill, ICard source) runningSkill = skillQueueObject.Dequeue();
            CardFacade skillFacade = new CardFacade(facadeData, runningSkill.source);
            //発動可能なら実行
            if (runningSkill.skill == null)
            {
                continue;
            }
            if (!runningSkill.skill.isSkillable(skillFacade))
            {
                Debug.Log(runningSkill.skill.name + ":Through");
                continue;
            }
            SkillCount++;

            if (SkillCount > 255)
            {
                Debug.Log("OverFlow!!!!");
                break;
            }
            //Skillを実行
            Debug.Log(runningSkill.skill.name + ":Skill");
            yield return runningSkill.skill.process(skillFacade).ToYieldInstruction();
        }
        isExecuting = false;
        // state.ChangeState(defaultState);
    }

    public void CardPlay(List<Skill> skills, ICard card)
    {
        skillQueueObject.PlayPush(skills, card);
        if (!skillQueueObject.Any())
        {
            Debug.Log("nulled");
            return;
        }
        if (!isExecuting)
        {
            //  state.ChangeState(skillingState);
            Debug.Log("SkilliStart");
            StartCoroutine("SkillExecute");
        }
        else
        {
            Debug.Log("PlayingNow");
        }
    }
}
