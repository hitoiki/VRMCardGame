using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillQueueObject
{
    // SkillProcessをスタックしていくキュー
    public Queue<(Skill skill, SkillTarget target)> skillQueue { get; } = new Queue<(Skill skill, SkillTarget target)>();

    public void Push(List<Skill> skills, IDealableCard Source, IDealableCard[] Target)
    {
        if (!skills.Any()) return;
        Debug.Log("Pushed");
        SkillTarget target = new SkillTarget(Source, Target);
        foreach (Skill s in skills)
        {
            skillQueue.Enqueue((s, target));
        }
    }
}


