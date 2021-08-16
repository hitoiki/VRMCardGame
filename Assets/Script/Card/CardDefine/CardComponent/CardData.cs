using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "CardData")]
public class CardData : ScriptableObject
{
    //カードを取り扱うクラス
    //基本的にここには静的データと、呼び出される処理を書く
    //効果はデコレーターを重ねて作る感じだろうか？
    public CardType type { get; }
    public string cardName;
    [TextArea] public string text;
    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite iconSprite;

    //効果いちらん、何らかの専用言語を作って構成したい
    public List<IUseEffect> useEffects;
}

public enum CardType
{
    Item, Event
}
