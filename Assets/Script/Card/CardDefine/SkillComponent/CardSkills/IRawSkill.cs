using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IRawSkill
{
    //実行効果のみ持つ、生のSkill
    //カードがRawSkillを持つことはないが、他のSkillによって使用される
    SkillProcess UseSkill();
}