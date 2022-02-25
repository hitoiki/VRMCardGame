using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillProcessCoin : ISkillProcess<(Coin c, int n)>, ISkillProcessTag
{
}
