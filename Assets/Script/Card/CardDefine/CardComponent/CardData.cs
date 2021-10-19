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
    public CardType[] type;
    public short cost;
    [TextArea] public string flavorText;
    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite iconSprite;

    //効果を入れるクラス
    public List<SkillComponent> skillComponents;
    public string CardText()
    {
        if (!skillComponents.Any())
        {
            Debug.Log("nullCardsText");
            return "";
        }
        return skillComponents.Select(x => { return x.GetText(); }).Aggregate((str1, str2) => str1 + str2);
    }

}

public enum CardType
{
    Item, Event
}
