using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardPrintable : ICardViewable
{
    IPermanent GetPermanent();
    Transform GetTransform();
    //アンカーを定める。主にエフェクトで散らばる時など
    void SetAnchor(Vector3 position);
    Vector3 GetAnchor();
}
