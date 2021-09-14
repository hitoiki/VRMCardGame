using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardPrinted
{
    void Print(Card card);
    void Active(bool active);
}
