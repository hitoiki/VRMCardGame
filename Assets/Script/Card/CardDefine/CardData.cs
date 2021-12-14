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

    public string cardName;
    public Suit suit;
    public short cost;
    [TextArea] public string flavorText;
    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite iconSprite;

    //効果を入れるクラス
    [SerializeField] public SkillPack skillPack;

    public string CardText()
    {
        return skillPack.SkillText() + "\n(" + flavorText + ")";
    }

}

public enum Suit
{
    Red, Vioret, Cyan, White//暫定的なスート、カッコいいのを見つけたら刷新する
}
