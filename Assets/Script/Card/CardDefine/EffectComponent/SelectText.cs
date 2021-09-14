using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectText : ScriptableObject
{
    public abstract string Text();
    public abstract void Effect(CardDealer dealer, Card source ,Card target);
}