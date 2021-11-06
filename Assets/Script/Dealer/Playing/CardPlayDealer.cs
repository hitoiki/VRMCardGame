using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using System;
public class CardPlayDealer : MonoBehaviour
{
    //CardSkillをStackに入れて順次実行するクラス

    [SerializeField] private StateDealer state;
    [SerializeField] private string skillingState;
    [SerializeField] private string defaultState;
    [SerializeField] private Stage stage;
    SkillQueueObject skillQueueObject => stage.queueObject;
    [SerializeField] FacadeData facadeData;
    private int SkillCount;
    private bool isExecuting = false;

    public void SkillQueueExecute()
    {
        SkillCount = 0;
        if (!skillQueueObject.skillQueue.Any())
        {
            Debug.Log("nulled");
            return;
        }
        //  state.ChangeState(skillingState);
        Debug.Log("SkillingState");
        if (!isExecuting) StartCoroutine("SkillExecute");
        else Debug.Log("Now Executing");
    }
    //SkillをQueueでまとめて、それを処理するコルーチンをObservable化したEffectで回す
    private IEnumerator SkillExecute()
    {
        isExecuting = true;

        while (skillQueueObject.skillQueue.Any())
        {
            SkillCount++;

            if (SkillCount > 99)
            {
                Debug.Log("OverFlow!!!!");
                break;
            }

            (Skill skill, SkillTarget target) runningSkill = skillQueueObject.skillQueue.Dequeue();
            List<IObservable<Unit>> effectEvents = new List<IObservable<Unit>>();
            Debug.Log(SkillCount.ToString() + ":Effect");
            foreach (ISkillEffect e in runningSkill.skill.effect.Where(x => { return x != null; }))
            {
                effectEvents.Add(e.Effect(runningSkill.target));
            }
            yield return Observable.WhenAll(effectEvents).First().ToYieldInstruction();
            Debug.Log(SkillCount.ToString() + ":Skill");
            runningSkill.skill.process.skill(new CardFacade(facadeData, runningSkill.target));
        }

        isExecuting = false;

        Debug.Log("DefaultState");
        // state.ChangeState(defaultState);
    }

    public void CardPlay(List<Skill> skills, IDealableCard Source, IDealableCard[] Target)
    {
        skillQueueObject.Push(skills, Source, Target);
        SkillQueueExecute();
        //state.ChangeState(skillingState);
    }
}
