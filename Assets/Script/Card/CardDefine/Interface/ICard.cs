using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public interface ICard : IDisposable
{
    //Cardをそのまま扱うのではなく、一旦interfaceを挟んでおく
    CardData GetCardData();
    Dictionary<Coin, int> GetCoin();
    //Coinを購読して色々やる
    IReadOnlyReactiveDictionary<Coin, int> GetObserveCoin();
    //これはCardの効果ではなく、Cardと追加効果を合わせたものを返す
    SkillPack GetSkillPack();
    void SetSecondSkillPack(SkillPack packSet);
    void AddSecondSkillPack(SkillPack packSet);
    void ChangeCoin(Coin c, int n);
    void MoveDeck(IDeck deck);
    //Effectが起こった際に購読する
    EffectProjector GetEffectProjector();
    void SetEffectProjector(EffectProjector projector);
}
