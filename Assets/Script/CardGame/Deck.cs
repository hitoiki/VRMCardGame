using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Deck : SupervisedObject
{
    public List<Card> cards;
    public List<Card> disCards;

    public List<Card> shuffle(List<Card> c)
    {
        return c.OrderBy(a => Guid.NewGuid()).ToList();

    }

    public List<Card> Draw(int i)
    {
        if (0 <= i)
        {
            if (i <= cards.Count)
            {
                List<Card> returnCards = cards.GetRange(0, i - 1);
                cards = cards.GetRange(i, cards.Count);
                return returnCards;
            }
            else
            {
                disCards = shuffle(disCards);
                cards.AddRange(disCards);
                List<Card> returnCards = cards.GetRange(0, i - 1);
                cards = cards.GetRange(i, cards.Count);
                return returnCards;

            }
        }
        return null;
    }

    public Card OneDraw()
    {
        if (0 <= cards.Count)
        {
            Card returnCard = cards.First();
            cards = cards.GetRange(1, cards.Count);
            return returnCard;
        }
        else
        {
            disCards = shuffle(disCards);
            cards.AddRange(disCards);
            Card returnCard = cards.First();
            cards = cards.GetRange(1, cards.Count);
            return returnCard;

        }

    }



}
