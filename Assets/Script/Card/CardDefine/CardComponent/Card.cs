﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


[System.Serializable]
public class Card
{
    // 実際に扱われるカード
    //エフェクトの欄をDataに足す

    public CardData mainData;
    private List<SkillPack> mainSkills => mainData.skillPack;
    public List<CardData> underCards = new List<CardData>();
    private List<SkillPack> underSkills => underCards.SelectMany(x => { return x.skillPack; }).ToList();

    delegate Skill SkillDeal(SkillPack text);

    public Card(CardData d)
    {
        mainData = d;
    }


    public string CardText()
    {
        string str = "";
        str += mainData.cardName + "「" + mainData.CardText() + "」";
        foreach (CardData data in underCards) str += "\n" + data.cardName + "「" + mainData.CardText() + "」";
        return str;
    }

    //CardDetaの"Text"を読み取って効果を発動する
    //CoinはCoinの変更時に
    //Dealerとかで発動タイミングの統括を図ったほうが良いような感じもする
    //非同期処理でないはずなので多分大丈夫きっと恐らく

    private bool PhaseCheck(SkillPack component, SkillPhase phase)
    {
        return component.GetCondition().activePhase == phase;
    }

    private List<Skill> SkillListRun(SkillDeal type)
    {
        //SkillTextから状況に応じてCardSkillを抽出する
        IEnumerable<Skill> mainSkill = mainData.skillPack
        .Where(y => { return (PhaseCheck(y, SkillPhase.top) || PhaseCheck(y, SkillPhase.always)); })
        .Select(y => { return type(y); }).Where(x => { return x != null; });

        IEnumerable<Skill> underSkill = underSkills
        .Where(y => { return (PhaseCheck(y, SkillPhase.under) || PhaseCheck(y, SkillPhase.always)); })
        .Select(y => { return type(y); }).Where(x => { return x != null; });

        return mainSkill.Concat(underSkill).ToList();
    }

    public List<Skill> CoinSkill(Coin coin, int n)
    {
        return SkillListRun(x => { return x.GetCoinSkill(coin, n); });
    }

    public List<Skill> UseSkill()
    {
        return SkillListRun(x => { return x.GetUseSkill(); });
    }

    public List<Skill> DrawSkill(StageDeck from, StageDeck to)
    {
        return SkillListRun(x => { return x.GetDrawSkill(from, to); });
    }

    public bool IsPlayable(Stage data)
    {
        bool mainSkillPlayable = mainData.skillPack
         .Where(y => { return PhaseCheck(y, SkillPhase.top) || PhaseCheck(y, SkillPhase.always); })
         .Aggregate(true, (b, skill) => { return b && skill.IsPlayable(data); });

        bool underSkillPlayable = underSkills
        .Where(y => { return PhaseCheck(y, SkillPhase.under) || PhaseCheck(y, SkillPhase.always); })
        .Aggregate(true, (b, skill) => { return b && skill.IsPlayable(data); });

        return mainSkillPlayable && underSkillPlayable;
    }

    public List<ICardChecking> PlayPrepare(Stage data)
    {
        IEnumerable<ICardChecking> mainSkill = mainData.skillPack
          .Where(y => { return PhaseCheck(y, SkillPhase.top) || PhaseCheck(y, SkillPhase.always); })
          .Select(y => { return y.PlayPrepare(data); });

        IEnumerable<ICardChecking> underSkill = underSkills
        .Where(y => { return PhaseCheck(y, SkillPhase.under) || PhaseCheck(y, SkillPhase.always); })
          .Select(y => { return y.PlayPrepare(data); });

        return mainSkill.Concat(underSkill).ToList();
    }

}
