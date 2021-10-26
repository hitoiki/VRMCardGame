using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillQueueObject : MonoBehaviour
{
    // SkillProcessをスタックしていくキュー
    public Queue<Skill> skillQueue = new Queue<Skill>();

    public void Push(IEnumerable<Skill> skills)
    {
        if (!skills.Any()) return;
        Debug.Log("Pushed");
        foreach (Skill s in skills)
        {
            skillQueue.Enqueue(s);
        }
    }
}
