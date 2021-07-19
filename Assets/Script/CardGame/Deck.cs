using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Deck : MonoBehaviour
{
#pragma warning disable 0649
    //カードを纏める所
    public List<Card> cards;

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
                List<Card> returnCards = cards;
                cards = null;
                return returnCards;

            }
        }
        return null;
    }

    public Card OneDraw()
    {
        Card returncard = Draw(1).First();
        return returncard;
    }



}
