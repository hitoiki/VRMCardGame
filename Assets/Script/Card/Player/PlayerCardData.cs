using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

public class PlayerCardData
{
    //Playerのデータ
    //ダメージを受けたりする

    //雑にUnderCardとコイン周りだけ引っ張って来た
    public List<CardData> underCards = new List<CardData>();
    private List<SkillPack> underSkills => underCards.SelectMany(x => { return x.skillPack; }).ToList();
    delegate Skill SkillDeal(SkillPack text);
    private bool PhaseCheck(SkillPack component, SkillPhase phase)
    {
        return component.GetCondition().activePhase == phase;
    }

    private List<Skill> SkillListRun(SkillDeal type)
    {
        //SkillTextから状況に応じてCardSkillを抽出する
        IEnumerable<Skill> underSkill = underSkills
        .Where(y => { return (PhaseCheck(y, SkillPhase.under) || PhaseCheck(y, SkillPhase.always)); })
        .Select(y => { return type(y); }).Where(x => { return x != null; });

        return underSkill.ToList();
    }

    public List<Skill> CoinSkill(Coin coin, int n)
    {
        return SkillListRun(x => { return x.GetCoinSkill(coin, n); });
    }

    public List<ICardChecking> PlayPrepare(Stage data)
    {
        IEnumerable<ICardChecking> underSkill = underSkills
        .Where(y => { return PhaseCheck(y, SkillPhase.under) || PhaseCheck(y, SkillPhase.always); })
          .Select(y => { return y.PlayPrepare(data); });

        return underSkill.ToList();
    }


}
