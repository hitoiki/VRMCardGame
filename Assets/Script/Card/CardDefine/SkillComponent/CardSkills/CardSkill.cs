using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSkill
{
    //Cardの処理を示すクラス
    public SkillPriority priority;
    public SkillPhase activePhase;
    public delegate void SkillType(CardDealer dealer);
    public SkillType skill;
    public CardSkill(SkillPriority p, SkillPhase a, SkillType s)
    {
        priority = p;
        activePhase = a;
        skill = s;
    }


}

public enum SkillPriority
{
    //Skillの発動順を示すenum
    //別にCoinEffect以外でも使うけどCoinに準じた名前にしておく
    beforeCoin, afterCoin
}

public enum SkillPhase
{
    //underCardかを示すenum
    always, top, under, Composite
}

//SkillTextから効果を抽出して実行
/*
効果
*/