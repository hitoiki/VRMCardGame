using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillQueueObject
{
    // SkillProcessをスタックしていくキュー
    private Queue<(Skill skill, SkillTarget target)> skillQueue = new Queue<(Skill skill, SkillTarget target)>();
    private Queue<(List<Skill> skills, SkillTarget target)> playQueue = new Queue<(List<Skill> skills, SkillTarget target)>();

    public void Push(List<Skill> skills, IDealableCard Source, IDealableCard[] Target)
    {
        if (!skills.Any()) return;
        SkillTarget target = new SkillTarget(Source, Target);
        foreach (Skill s in skills)
        {
            skillQueue.Enqueue((s, target));
        }
    }

    public void Push(List<Skill> skills, SkillTarget target)
    {
        if (!skills.Any()) return;
        foreach (Skill s in skills)
        {
            skillQueue.Enqueue((s, target));
        }
    }

    public (Skill skill, SkillTarget target) Dequeue()
    {
        Debug.Log("Dequeue");
        if (!skillQueue.Any())
        {
            Debug.Log("Push");
            var play = playQueue.Dequeue();
            Push(play.skills, play.target);
        }
        return skillQueue.Dequeue();
    }

    public bool Any()
    {
        return skillQueue.Any() || playQueue.Any();
    }

    public void PlayPush(List<Skill> skills, IDealableCard Source, IDealableCard[] Target)
    {
        if (!skills.Any()) return;
        SkillTarget target = new SkillTarget(Source, Target);
        playQueue.Enqueue((skills, target));
    }
    public void PlayPush(List<Skill> skills, SkillTarget target)
    {
        if (!skills.Any()) return;
        playQueue.Enqueue((skills, target));

    }
}


