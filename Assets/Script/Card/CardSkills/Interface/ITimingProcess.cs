using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimingProcess
{
    //ターン終了時など、その他特定の状況で発動する
    void GetSkillProcess(CardFacade facede, SkillTiming t);
    bool GetIsSkillable(CardFacade facede, SkillTiming t);
}

public enum SkillTiming
{
    turnEnd
}
