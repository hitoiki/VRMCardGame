using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
#pragma warning disable 0649
public class CardField : MonoBehaviour
{
    //Cardを置く盤面、舞台
    //コイツを読んで処理したりしなかったりする
    [SerializeField] public Deck deck;
    [SerializeField] public Deck hands;
    [SerializeField] public Deck field;
    public Card draft;

    public void OnClick()
    {
        hands.Add(draft);
    }
}
