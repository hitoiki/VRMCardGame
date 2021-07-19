using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CardField : MonoBehaviour
{
    //Cardを置く盤面、舞台
    //コイツを読んで処理したりしなかったりする
    [SerializeField] private Deck _deck;
    public ReactiveProperty<Deck> deck => new ReactiveProperty<Deck>(_deck);
    public ReactiveProperty<Deck> hands => new ReactiveProperty<Deck>();
    public ReactiveProperty<Deck> field => new ReactiveProperty<Deck>();
    public Card draft;

    private void Start()
    {
        deck.Subscribe(x => { _deck = x; });
    }
}
