using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDealableCard
{
    //Cardをそのまま扱うのではなく、一旦interfaceを挟んでおく
    Card GetCard();
    CoinSet GetCoin();
    //これはCardの効果ではなく、Cardと追加効果を合わせたものを返す
    SkillPack GetSkillPack();

    void SetCard(Card card);
    void SetSecondSkillPack(SkillPack packSet);
    void AddSecondSkillPack(SkillPack packSet);
}
