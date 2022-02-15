using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPermanent
{
    //フォロワー、エンチャントみたいなもの
    //Deckはこれを管轄する いわばDeck上のICardの書式
    ICard GetCard();
    void SetCard(ICard card);
    IDeck OnDeck();
    void ChangeCoin(Coin c, int n);
    void MoveDeck(IDeck deck);
    //Effectが起こった際に購読する
    EffectProjector GetEffectProjector();

    //Coin購読用の奴 IReadOnlyReactiveDictionary<Coin, int> GetObserveCoin();
}
