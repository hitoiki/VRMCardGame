using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public Card sourceCard;
    public SkillEffect effect;
    public SkillProcess process;

    public Skill(Card Source, SkillEffect Effect, SkillProcess Process)
    {
        sourceCard = Source;
        this.effect = Effect;
        this.process = Process;

    }
}
