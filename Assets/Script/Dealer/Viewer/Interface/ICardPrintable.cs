using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardPrintable
{
    void Print(Card card);
    //仮に購読していたのなら、それを解除
    void UnPrint();
    void Active(bool active);
}
