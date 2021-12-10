using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class SelectDeckCardChecking : ICardChecking
{
    DeckType stageDeck;
    public SelectDeckCardChecking(DeckType deck)
    {
        stageDeck = deck;
    }
    public DeckType GetDeck()
    {
        return stageDeck;
    }
}
