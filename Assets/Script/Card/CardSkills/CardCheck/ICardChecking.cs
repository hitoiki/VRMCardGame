using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICardChecking
{
    //対象を選ぶ時等、CardCheckerで行う処理
    DeckType GetDeck();
}
