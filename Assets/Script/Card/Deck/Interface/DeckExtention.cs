using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeckExtention
{
    public static List<IPermanent> Add(this IDeck deck, List<ICard> cards)
    {
        List<IPermanent> l = new List<IPermanent>();
        foreach (ICard c in cards)
        {
            l.Add(deck.Add(c));
        }
        return l;
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
    public static ICard Pick(this IDeck deck, ICard card)
    {
        if (deck.ExistCheck(card))
        {
            //存在を調べてから除いている事を明確にするために二重入れ子で表現
            if (deck.Remove(card)) return card;
        }
        return null;
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

    public static List<IPermanent> AddPack(this IDeck deck, Pack pack)
    {
        List<IPermanent> l = new List<IPermanent>();
        foreach (ICard c in pack.GetCards())
        {
            l.Add(deck.Add(c));
        }
        return l;
    }

}
