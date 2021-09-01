using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Data", menuName = "CardData")]
public class CardData : ScriptableObject, IUseEffect, IDrawEffect
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

    //効果を入れるクラス
    public UseText useText;
    public CoinText coinText;
    public DrawText drawText;
    public SelectText selectText;

    //一応IEffectに対応させておく
    public void UseEffect(CardDealer dealer) { if (useText != null) useText.Effect(dealer, new Card(this)); }
    //public void CoinEffect(CardDealer dealer, Coin coin, short n) { if (coinText != null) coinText.Effect(dealer, new Card(this), coin, n); }
    public void DrawEffect(CardDealer dealer, StageDeck from, StageDeck to) { if (drawText != null) drawText.Effect(dealer, new Card(this), from, to); }

    public string CardText(){
        string str = "";
        if(useText!= null) str += useText.Text();
        if(coinText!= null) str += coinText.Text();
        if(drawText!= null) str += drawText.Text();
        if(selectText!= null) str += selectText.Text();
        return str;
        }
        
}

public enum CardType
{
    Item, Event
}
