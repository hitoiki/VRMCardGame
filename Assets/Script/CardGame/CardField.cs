﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
#pragma warning disable 0649
public class CardField : MonoBehaviour
{
    //Cardを置く盤面、舞台
    //コイツを読んで処理したりしなかったりする
    [SerializeField] private Deck _deck;
    public ReactiveProperty<Deck> deck => new ReactiveProperty<Deck>(_deck);
    [SerializeField] private Deck _hands;
    public ReactiveProperty<Deck> hands => new ReactiveProperty<Deck>(_hands);
    [SerializeField] private Deck _field;
    public ReactiveProperty<Deck> field => new ReactiveProperty<Deck>(_field);
    public Card draft;

    private void Start()
    {
        deck.Subscribe(x => { _deck = x; });
    }
}
