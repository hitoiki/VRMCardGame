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
    [SerializeField] private EffectStateLinker linker;
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
            (Skill skill, SkillDealableCard source, List<SkillDealableCard> target) runningSkill = skillQueueObject.Dequeue();
            CardFacade skillFacade = new CardFacade(facadeData, runningSkill.source, runningSkill.target);
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
            //Effectを発動する場所の定義
            EffectLocation location = runningSkill.source.GetEffectTarget();
            if (runningSkill.target != null && runningSkill.target.Any())
            {
                foreach (SkillDealableCard skill in runningSkill.target)
                {
                    skill.AddTarget(location);
                }
            }
            //Effectを実行して、終わるまで待機
            List<IObservable<Unit>> effectEvents = new List<IObservable<Unit>>();
            Debug.Log(runningSkill.skill.name + ":Effect");

            if (runningSkill.skill.effect.Any())
            {
                foreach (ISkillEffect e in runningSkill.skill.effect.Where(x => { return x != null; }))
                {
                    linker.effects.Add(e);
                    effectEvents.Add(location.Effect(e));

                }
                yield return Observable.WhenAll(effectEvents).First().ToYieldInstruction();
                //EffectをLinkerから外す
                foreach (ISkillEffect e in runningSkill.skill.effect.Where(x => { return x != null; }))
                {
                    linker.effects.Remove(e);
                }
            }
            //Skillを実行
            Debug.Log(runningSkill.skill.name + ":Skill");
            IObservable<Unit> skillEvent = runningSkill.skill.process(skillFacade);
            yield return skillEvent.ToYieldInstruction();

        }

        isExecuting = false;

        Debug.Log("DefaultState");
        // state.ChangeState(defaultState);
    }

    public void CardPlay(List<Skill> skills, SkillDealableCard source, List<SkillDealableCard> target)
    {
        skillQueueObject.PlayPush(skills, source, target);
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
    public void PrintedCardPlay(List<Skill> skills, ICardPrintable printable, IDeck deck)
    {
        skillQueueObject.PlayPush(skills, new SkillDealableCard(printable, deck, stage.queueObject), null);
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
    public void PrintedCardPlay(List<Skill> skills, ICardPrintable printable, IDeck deck, List<(ICardPrintable, IDeck)> targetPrintable)
    {
        skillQueueObject.PlayPush(skills, new SkillDealableCard(printable, deck, stage.queueObject), targetPrintable.Select(x => { return new SkillDealableCard(x.Item1, x.Item2, stage.queueObject); }).ToList());
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
