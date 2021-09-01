using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CoinText : ScriptableObject
{
    public abstract string Text();
    public abstract void Effect(CardDealer dealer, Card source, Coin c, short n);
}
public abstract class UseText : ScriptableObject
{
    public abstract string Text();
    public abstract void Effect(CardDealer dealer, Card source);
}
public abstract class DrawText : ScriptableObject
{
    public abstract string Text();
    public abstract void Effect(CardDealer dealer, Card source, StageDeck from, StageDeck to);
}

public abstract class SelectText : ScriptableObject
{
    public abstract string Text();
    public abstract void Effect(CardDealer dealer, Card source ,Card target);
}