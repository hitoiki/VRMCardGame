using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardViewable
{
    //描画のみ出来るinterfaceを分離
    void Print(ICard card);
    //仮に購読していたのなら、それを解除
    void UnPrint();
    void Active(bool active);
}
