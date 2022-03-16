using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.IO;
[CreateAssetMenu(fileName = "Data", menuName = "CardData")]
public class CardData : ScriptableObject
{
    //カードを取り扱うクラス
    //基本的にここには静的データと、呼び出される処理を書く
    //ここは呼び出すだけに努めること
    public string textName;
    public Coin costCoin;
    public short cost;
    public Suit suit;
    [TextArea] public string flavorText;
    public Sprite backSprite;
    public Sprite frontSprite;
    public Sprite iconSprite;
    [PathAttribute] public string poseJsonFilePath = "";
    public PoseItem poseItem;
    //効果を入れるクラス
    public SkillPack skillPack;

    public string CardText()
    {
        if (flavorText != "") return skillPack.SkillText() + "\n(" + flavorText + ")";
        else return skillPack.SkillText();
    }
    private void OnValidate()
    {
        PoseSet();
    }

    [ContextMenu("PoseSet")]
    private void PoseSet()
    {
        if (poseJsonFilePath == "") return;
        string json = File.ReadAllText(poseJsonFilePath);
        poseItem = JsonUtility.FromJson<PoseItem>(json);
    }


}
public enum Suit
{
    Red, Violet, Cyan, White
}
