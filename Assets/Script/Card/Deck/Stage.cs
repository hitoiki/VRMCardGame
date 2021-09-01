using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
#pragma warning disable 0649
public class Stage : MonoBehaviour
{
    //Cardを置く盤面、舞台
    //コイツを読んで処理したりしなかったりする
    [SerializeField] public Deck hands;
    [SerializeField] public Deck field;
    [SerializeField] public Deck disCard;
    [SerializeField] public Deck trace;
    [SerializeField] public Deck Senter;
    [SerializeField] public Deck Right;
    [SerializeField] public Deck Left;

    public Deck DeckKey(StageDeck e)
    {
        if (e == StageDeck.hands) return hands;
        if (e == StageDeck.field) return field;
        if (e == StageDeck.discard) return disCard;
        if (e == StageDeck.trace) return trace;
        if (e == StageDeck.senter) return Senter;
        if (e == StageDeck.right) return Right;
        if (e == StageDeck.left) return Left;

        return null;

    }

}

public enum StageDeck
{
    hands, field, discard, trace, senter, right, left
}
