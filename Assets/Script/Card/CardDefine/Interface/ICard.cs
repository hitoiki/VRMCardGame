using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public interface ICard
{
    //Cardをそのまま扱うのではなく、一旦interfaceを挟んでおく
    CardData GetCardData();
    Dictionary<Coin, int> GetCoin();
    //これはCardの効果ではなく、Cardと追加効果を合わせたものを返す
    SkillPack GetSkillPack();
    void SetSecondSkillPack(SkillPack packSet);
    void AddSecondSkillPack(SkillPack packSet);
}
