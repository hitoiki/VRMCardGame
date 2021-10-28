using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillQueueObject : MonoBehaviour
{
    // SkillProcessをスタックしていくキュー
    public Queue<(Skill skill, EffectTarget target)> skillQueue { get; } = new Queue<(Skill skill, EffectTarget target)>();

    public void Push(List<Skill> skills, IDealableCard Source, IDealableCard[] Target)
    {
        if (!skills.Any()) return;
        Debug.Log("Pushed");
        EffectTarget target = new EffectTarget(Source, Target);
        foreach (Skill s in skills)
        {
            skillQueue.Enqueue((s, target));
        }
    }
}


