using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHighOrderSkill
{
    // Skillに対するSkill
    // Skillが実行される時に反応して、そのSkillに色々行う

    bool SkillCheck(Skill process);
    Skill HighOrderSkill(Skill process);
}