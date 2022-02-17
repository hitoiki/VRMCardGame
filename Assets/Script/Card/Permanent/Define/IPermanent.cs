using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPermanent : IDisposable
{
    //フォロワー、エンチャントみたいなもの
    //Deckはこれを管轄する いわばDeck上のICardの書式
    ICard GetCard();
    void SetCard(ICard card);
    IDeck OnDeck();
    (Coin coin, int result) ChangeCoin(Coin c, int n);
    bool MoveDeck(IDeck deck);
    //Effectが起こった際に購読する
    EffectProjector GetEffectProjector();

    //Coin購読用の奴 IReadOnlyReactiveDictionary<Coin, int> GetObserveCoin();
}
