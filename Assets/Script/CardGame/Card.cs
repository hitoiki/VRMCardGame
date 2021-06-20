using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Card")]
public class Card : ScriptableObject
{
    // カードゲーム作ることにした
    //Cardのクラスと、それを扱うデッキを作る
    //仮組なので文字列とシャッフルできればOK
    //Actionじゃないって？しらん
    public CardType type;
    public string cardName;
    public string text;
    public Sprite sprite;

    void Using()
    {

    }

}

public enum CardType
{
    Item, Event
}
