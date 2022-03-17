using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillProcessCheck : ISkillProcessTag
{
    //手札から使われる能力が使用可能か判断するクラス。
    bool IsPlayable(CardFacade facade);
}
