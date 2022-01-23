using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeckExtention
{
    public static bool Add(this IDeck deck, List<ICard> cards)
    {
        bool b = true;
        foreach (ICard c in cards)
        {
            b = b && deck.Add(c);
        }
        return b;
    }
    public static bool Remove(this IDeck deck, List<ICard> cards)
    {
        bool b = true;
        foreach (ICard c in cards)
        {
            b = b && deck.Remove(c);
        }
        return b;
    }
    public static List<ICard> Pick(this IDeck deck, List<ICard> cs)
    {
        List<ICard> returnCards = new List<ICard>();
        foreach (ICard c in cs)
        {
            if (deck.Pick(c) != null) returnCards.Add(c);
        }
        return returnCards;
    }

}
