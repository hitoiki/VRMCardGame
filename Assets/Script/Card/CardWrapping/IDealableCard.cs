using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDealableCard
{
    //Cardそのまま扱うのではなく、一旦interfaceを挟んでおく
    Transform GetTransform();
    Card GetCard();

    void SetCard(Card card);
    void SetTransform(Transform transform);
}
