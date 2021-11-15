using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class SelectDeckCardChecking : ICardChecking
{
    StageDeck stageDeck;
    public SelectDeckCardChecking(StageDeck deck)
    {
        stageDeck = deck;
    }
    public StageDeck GetDeck()
    {
        return stageDeck;
    }
}
