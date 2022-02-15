using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillQueue
{
    // SkillProcessをスタックしていくキュー
    private Queue<(Skill skill, IPermanent source)> skillQueue = new Queue<(Skill skill, IPermanent source)>();
    private Queue<(List<Skill> skills, IPermanent source)> playQueue = new Queue<(List<Skill> skills, IPermanent source)>();

    public void Push(List<Skill> skills, IPermanent source)
    {
        if (!skills.Any()) return;
        foreach (Skill s in skills)
        {
            skillQueue.Enqueue((s, source));
        }
    }
    public (Skill skill, IPermanent source) Dequeue()
    {
        Debug.Log("Dequeue");
        if (!skillQueue.Any())
        {
            var play = playQueue.Dequeue();
            if (play.skills != null) Push(play.skills, play.source);
        }
        return skillQueue.Dequeue();
    }

    public bool Any()
    {
        return skillQueue.Any() || playQueue.Any();
    }
    public void PlayPush(List<Skill> skills, IPermanent source)
    {
        if (!skills.Any()) return;
        playQueue.Enqueue((skills, source));

    }
}


