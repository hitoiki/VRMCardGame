using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardDataExtention
{
    public static string SuitName(this Suit suit)
    {
        if (suit == Suit.Cyan) return "Cyan";
        if (suit == Suit.Red) return "Red";
        if (suit == Suit.Violet) return "Violet";
        if (suit == Suit.White) return "White";
        return "";
    }
    public static string Text(this Suit suit)
    {
        if (suit == Suit.Cyan) return "青";
        if (suit == Suit.Red) return "赤";
        if (suit == Suit.Violet) return "紫";
        if (suit == Suit.White) return "白";
        return "";
    }

}
