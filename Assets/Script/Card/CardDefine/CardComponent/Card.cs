using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Card")]
public class Card : ScriptableObject
{
    //カードを取り扱うクラス
    //基本的にここには静的データと、呼び出される処理を書く
    //効果はデコレーターを重ねて作る感じだろうか？

    //CardDealerがいちいち読み取ってカードを処理する
    //だからこの条件ならこの効果を発動しますよって形式であってほしい
    //Linqを安直に挟むか？
    public CardType type { get; }
    public string cardName;
    [TextArea] public string text;
    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite iconSprite;

    public void cardUse()
    {

    }
}

public enum CardType
{
    Item, Event
}
