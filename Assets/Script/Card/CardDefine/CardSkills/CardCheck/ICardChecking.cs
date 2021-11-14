using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardChecking
{
    //対象を選ぶ時等、CardCheckerで行う処理
    void Check(CardPlayChecker checker);
}
