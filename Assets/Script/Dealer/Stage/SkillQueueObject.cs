using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillQueueObject
{
    // SkillProcessをスタックしていくキュー
    private Queue<(Skill skill, EffectTarget target)> skillQueue = new Queue<(Skill skill, EffectTarget target)>();
    private Queue<(List<Skill> skills, EffectTarget target)> playQueue = new Queue<(List<Skill> skills, EffectTarget target)>();
    public void Push(List<Skill> skills, ICardPrintable Source, ICardPrintable[] Target)
    {
        if (!skills.Any()) return;
        EffectTarget target = new EffectTarget(Source, Target);
        foreach (Skill s in skills)
        {
            skillQueue.Enqueue((s, target));
        }
    }

    public void Push(List<Skill> skills, EffectTarget target)
    {
        if (!skills.Any()) return;
        foreach (Skill s in skills)
        {
            skillQueue.Enqueue((s, target));
        }
    }
    public (Skill skill, EffectTarget target) Dequeue()
    {
        Debug.Log("Dequeue");
        if (!skillQueue.Any())
        {
            var play = playQueue.Dequeue();
            if (play.skills != null) Push(play.skills, play.target);
        }
        return skillQueue.Dequeue();
    }

    public bool Any()
    {
        return skillQueue.Any() || playQueue.Any();
    }

    public void PlayPush(List<Skill> skills, ICardPrintable Source, ICardPrintable[] Target)
    {
        if (!skills.Any()) return;
        EffectTarget target = new EffectTarget(Source, Target);
        playQueue.Enqueue((skills, target));
    }
    public void PlayPush(List<Skill> skills, EffectTarget target)
    {
        if (!skills.Any()) return;
        playQueue.Enqueue((skills, target));

    }
}


