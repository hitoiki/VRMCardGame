using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Data", menuName = "CardData")]
public class CardData : ScriptableObject
{
    //カードを取り扱うクラス
    //基本的にここには静的データと、呼び出される処理を書く
    //ここは呼び出すだけに努めること
    public string textName;
    public Coin costCoin;
    public short cost;
    public CardType type;
    public Suit suit;
    [TextArea] public string flavorText;
    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite iconSprite;
    //効果を入れるクラス
    public SkillPack skillPack;

    public string CardText()
    {
        if (flavorText != "") return skillPack.SkillText() + "\n(" + flavorText + ")";
        else return skillPack.SkillText();
    }

}

public enum CardType
{
    Item, Enemy
}

public enum Suit
{
    Red, Violet, Cyan, White
}
