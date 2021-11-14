using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using System;
public class CardPlayDealer : MonoBehaviour
{
    //CardSkillをStackに入れて順次実行するクラス

    /*いつか使いそうなのでコメントアウト
    [SerializeField] private StateDealer state;
    [SerializeField] private string skillingState;
    [SerializeField] private string defaultState;*/
    [SerializeField] private EffectStateLinker linker;
    [SerializeField] private Stage stage;
    SkillQueueObject skillQueueObject => stage.queueObject;
    [SerializeField] FacadeData facadeData;
    private int SkillCount;
    private bool isExecuting = false;

    //SkillをQueueでまとめて、それを処理するコルーチンをObservable化したEffectで回す
    private IEnumerator SkillExecute()
    {
        isExecuting = true;
        SkillCount = 0;
        while (skillQueueObject.Any())
        {
            SkillCount++;

            if (SkillCount > 99)
            {
                Debug.Log("OverFlow!!!!");
                break;
            }

            (Skill skill, SkillTarget target) runningSkill = skillQueueObject.Dequeue();
            List<IObservable<Unit>> effectEvents = new List<IObservable<Unit>>();
            Debug.Log(SkillCount.ToString() + ":Effect");
            if (runningSkill.skill.effect.Any())
            {
                foreach (ISkillEffect e in runningSkill.skill.effect.Where(x => { return x != null; }))
                {
                    linker.effects.Add(e);
                    effectEvents.Add(e.Effect(runningSkill.target));
                }
                yield return Observable.WhenAll(effectEvents).First().ToYieldInstruction();
                //EffectをLinkerから外す
                foreach (ISkillEffect e in runningSkill.skill.effect.Where(x => { return x != null; }))
                {
                    linker.effects.Remove(e);
                }
            }
            Debug.Log(SkillCount.ToString() + ":Skill");
            runningSkill.skill.process.skill(new CardFacade(facadeData, runningSkill.target));
        }

        isExecuting = false;

        Debug.Log("DefaultState");
        // state.ChangeState(defaultState);
    }

    public void CardPlay(List<Skill> skills, IDealableCard Source, IDealableCard[] Target)
    {
        skillQueueObject.PlayPush(skills, Source, Target);
        if (!skillQueueObject.Any())
        {
            Debug.Log("nulled");
            return;
        }
        if (!isExecuting)
        {
            //  state.ChangeState(skillingState);
            Debug.Log("SkillingState");
            StartCoroutine("SkillExecute");
        }

    }
}
