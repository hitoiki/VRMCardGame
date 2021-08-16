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
    [SerializeField] public Pack otherDeck;

    public Deck DeckKey(FieldDecksEnum e)
    {
        if (e == FieldDecksEnum.hands) return hands;
        if (e == FieldDecksEnum.field) return field;
        if (e == FieldDecksEnum.discard) return disCard;
        if (e == FieldDecksEnum.trace) return trace;
        return null;

    }

}

public enum FieldDecksEnum
{
    hands, field, discard, trace
}
