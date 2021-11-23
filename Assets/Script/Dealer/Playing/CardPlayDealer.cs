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

            //SkillをQueueから取り出し
            (Skill skill, EffectTarget target) runningSkill = skillQueueObject.Dequeue();
            CardFacade skillFacade = new CardFacade(facadeData, runningSkill.target.DealableSource(), runningSkill.target.DealableTarget());
            //発動可能なら実行
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
            //Effectを実行して、終わるまで待機
            List<IObservable<Unit>> effectEvents = new List<IObservable<Unit>>();
            Debug.Log(runningSkill.skill.name + ":Effect");
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
            //Skillを実行
            Debug.Log(SkillCount.ToString() + ":Skill");
            runningSkill.skill.process(skillFacade);

        }

        isExecuting = false;

        Debug.Log("DefaultState");
        // state.ChangeState(defaultState);
    }

    public void CardPlay(List<Skill> skills, ICardPrintable Source, ICardPrintable[] Target)
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
