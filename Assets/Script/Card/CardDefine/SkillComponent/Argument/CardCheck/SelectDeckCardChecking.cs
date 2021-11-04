using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDeckCardChecking : ICardChecking
{
    StageDeck stageDeck;
    sbyte amount;

    public SelectDeckCardChecking(StageDeck deck, sbyte amo)
    {
        stageDeck = deck;
        amount = amo;
    }
    public void Check(CardPlayChecker checker)
    {
        checker.SelectStageCard(stageDeck, amount);
    }
}
