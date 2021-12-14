using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public interface ICard
{
    //Cardをそのまま扱うのではなく、一旦interfaceを挟んでおく
    CardData GetCardData();
    Dictionary<Coin, int> GetCoin();
    IReadOnlyReactiveDictionary<Coin, int> GetObserveCoin();
    //これはCardの効果ではなく、Cardと追加効果を合わせたものを返す
    SkillPack GetSkillPack();
    void SetSecondSkillPack(SkillPack packSet);
    void AddSecondSkillPack(SkillPack packSet);
    void ChangeCoin(Coin c, int n);
}
