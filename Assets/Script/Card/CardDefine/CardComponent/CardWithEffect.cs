using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWithEffect : Card
{
    //ゲーム中に取り扱われるカード
    //追加される効果をいつか書いて置くぜ
    protected Card card;
    public CardWithEffect(Card c)
    {
        card = c;
    }
}
