using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillQueueObject
{
    // SkillProcessをスタックしていくキュー
    private Queue<(Skill skill, SkillDealableCard source, List<SkillDealableCard> target)> skillQueue = new Queue<(Skill skill, SkillDealableCard source, List<SkillDealableCard> target)>();
    private Queue<(List<Skill> skills, SkillDealableCard source, List<SkillDealableCard> target)> playQueue = new Queue<(List<Skill> skills, SkillDealableCard source, List<SkillDealableCard> target)>();

    public void Push(List<Skill> skills, SkillDealableCard source, List<SkillDealableCard> target)
    {
        if (!skills.Any()) return;
        foreach (Skill s in skills)
        {
            skillQueue.Enqueue((s, source, target));
        }
    }
    public (Skill skill, SkillDealableCard source, List<SkillDealableCard> target) Dequeue()
    {
        Debug.Log("Dequeue");
        if (!skillQueue.Any())
        {
            var play = playQueue.Dequeue();
            if (play.skills != null) Push(play.skills, play.source, play.target);
        }
        return skillQueue.Dequeue();
    }

    public bool Any()
    {
        return skillQueue.Any() || playQueue.Any();
    }
    public void PlayPush(List<Skill> skills, SkillDealableCard source, List<SkillDealableCard> target)
    {
        if (!skills.Any()) return;
        playQueue.Enqueue((skills, source, target));

    }
}


