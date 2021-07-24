﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Card")]
public class CardData : ScriptableObject
{
    //カードの静的データを取り扱うクラス
    //コイツ→Card→CardGeneと続く感じ
    public CardType type { get; }
    public string cardName;
    public string text;
    public Sprite sprite;

    public

    void Using()
    {

    }

}

public enum CardType
{
    Item, Event
}
