using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Data", menuName = "CardData")]
public class CardData : ScriptableObject, IUseSkill, IDrawSkill
{
    //カードを取り扱うクラス
    //基本的にここには静的データと、呼び出される処理を書く
    //ここは呼び出すだけに努めること

    public string cardName;
    public HashSet<CardType> type;
    public short cost;
    [TextArea] public string flavorText;
    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite iconSprite;

    //効果を入れるクラス
    public UseText useText;
    public CoinText coinText;
    public DrawText drawText;
    public SelectText selectText;

    //一応ISkillに対応させておく
    public void UseSkill(CardDealer dealer) { if (useText != null) useText.Skill(dealer, new Card(this)); }
    //public void CoinSkill(CardDealer dealer, Coin coin, short n) { if (coinText != null) coinText.Skill(dealer, new Card(this), coin, n); }
    public void DrawSkill(CardDealer dealer, StageDeck from, StageDeck to) { if (drawText != null) drawText.Skill(dealer, new Card(this), from, to); }

    public string CardText()
    {
        string str = "";
        if (useText != null) str += useText.Text();
        if (coinText != null) str += coinText.Text();
        if (drawText != null) str += drawText.Text();
        if (selectText != null) str += selectText.Text();
        return str;
    }

}

public enum CardType
{
    Item, Event
}
