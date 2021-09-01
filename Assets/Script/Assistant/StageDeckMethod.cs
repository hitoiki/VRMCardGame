using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StageDeckMethod
{
    public static string ToStringFast(StageDeck e){
        if (e == StageDeck.hands) return "hands";
        if (e == StageDeck.field) return "field";
        if (e == StageDeck.discard) return "disCard";
        if (e == StageDeck.trace) return "trace";
        if (e == StageDeck.senter) return "Senter";
        if (e == StageDeck.right) return "Right";
        if (e == StageDeck.left) return "Left";
        return null;
    }

    public static string ToCardText(StageDeck e){
        if (e == StageDeck.hands) return "手札";
        if (e == StageDeck.field) return "場";
        if (e == StageDeck.discard) return "捨て札";
        if (e == StageDeck.trace) return "跡札";
        if (e == StageDeck.senter) return "中央のデッキ";
        if (e == StageDeck.right) return "右のデッキ";
        if (e == StageDeck.left) return "左のデッキ";
        return null;
    }

}
