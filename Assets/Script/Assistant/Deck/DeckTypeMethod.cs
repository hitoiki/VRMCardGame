using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeckTypeMethod
{
    public static string ToStringFast(this DeckType e)
    {
        if (e == DeckType.hands) return "hands";
        if (e == DeckType.field) return "field";
        if (e == DeckType.discard) return "disCard";
        if (e == DeckType.trace) return "trace";
        if (e == DeckType.deck) return "Deck";
        if (e == DeckType.right) return "Right";
        if (e == DeckType.left) return "Left";
        return "";
    }

    public static string ToCardText(this DeckType e)
    {
        if (e == DeckType.hands) return "手札";
        if (e == DeckType.field) return "場";
        if (e == DeckType.discard) return "捨て札";
        if (e == DeckType.trace) return "跡札";
        if (e == DeckType.deck) return "デッキ";
        if (e == DeckType.right) return "右のデッキ";
        if (e == DeckType.left) return "左のデッキ";
        return "";
    }

}
