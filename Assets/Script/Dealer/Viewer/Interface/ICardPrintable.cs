using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardPrintable
{
    void Print(ICard card);
    //仮に購読していたのなら、それを解除
    void UnPrint();
    void Active(bool active);
    ICard GetCard();
    Transform GetTransform();
    //アンカーを定める。主にエフェクトで散らばる時など
    void SetAnchor(Vector3 position);
    Vector3 GetAnchor();
}
