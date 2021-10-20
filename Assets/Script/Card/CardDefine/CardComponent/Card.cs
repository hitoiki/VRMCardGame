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
    private List<SkillComponent> mainSkills => mainData.skillComponents;
    public List<CardData> underCards = new List<CardData>();
    private List<SkillComponent> underSkills => underCards.SelectMany(x => { return x.skillComponents; }).ToList();
    public ReactiveDictionary<Coin, short> coins = new ReactiveDictionary<Coin, short>();

    delegate CardSkill SkillDeal(SkillComponent text);

    public Card(CardData d)
    {
        mainData = d;
    }
    //効果が反応するようなCoin操作
    //Coinに反応してSkillが走ってそれにCoinが反応して…なのであんま良くない
    public void AddCoin(CardFacade dealer, Coin c, short n)
    {
        if (coins.ContainsKey(c)) coins[c] += n;
        else coins.Add(c, n);
    }

    public void RemoveCoin(CardFacade dealer, Coin c, short n)
    {
        if (coins.ContainsKey(c))
        {
            if (coins[c] > n) coins[c] = 0;
            else coins[c] -= n;
        }
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

    private bool PhaseCheck(SkillComponent component, SkillPhase phase)
    {
        return component.GetCondition().activePhase == phase;
    }

    private List<CardSkill> SkillListRun(SkillDeal type)
    {
        //SkillTextから状況に応じてCardSkillを抽出する
        IEnumerable<CardSkill> mainSkill = mainData.skillComponents
        .Where(y => { return (PhaseCheck(y, SkillPhase.top) || PhaseCheck(y, SkillPhase.always)); })
        .Select(y => { return type(y); }).Where(x => { return x != null; });

        IEnumerable<CardSkill> underSkill = underSkills
        .Where(y => { return (PhaseCheck(y, SkillPhase.under) || PhaseCheck(y, SkillPhase.always)); })
        .Select(y => { return type(y); }).Where(x => { return x != null; });

        return mainSkill.Concat(underSkill).ToList();
    }

    public List<CardSkill> CoinSkill(Coin coin, short n)
    {
        return SkillListRun(x => { return x.GetCoinSkill(this, coin, n); });
    }

    public List<CardSkill> UseSkill()
    {
        return SkillListRun(x => { return x.GetUseSkill(this); });
    }

    public List<CardSkill> DrawSkill(StageDeck from, StageDeck to)
    {
        return SkillListRun(x => { return x.GetDrawSkill(this, from, to); });
    }
    public List<CardSkill> SelectSkill(List<Card> target)
    {
        return SkillListRun(x => { return x.GetSelectSkill(this, target); });
    }

    public bool IsPlayable(GamePlayData data)
    {
        bool mainSkillPlayable = mainData.skillComponents
         .Where(y => { return PhaseCheck(y, SkillPhase.top) || PhaseCheck(y, SkillPhase.always); })
         .Aggregate(true, (b, skill) => { return b && skill.IsPlayable(data, this); });

        bool underSkillPlayable = underSkills
        .Where(y => { return PhaseCheck(y, SkillPhase.under) || PhaseCheck(y, SkillPhase.always); })
        .Aggregate(true, (b, skill) => { return b && skill.IsPlayable(data, this); });

        return mainSkillPlayable && underSkillPlayable;
    }

    public bool IsSelect(GamePlayData data)
    {
        bool IsMainSkillSelect = mainData.skillComponents.Aggregate(true, (b, skill) => { return b && skill.IsSelect(data); });
        bool IsUnderSkillSelect = underSkills.Aggregate(true, (b, skill) => { return b && skill.IsSelect(data); });
        return IsMainSkillSelect && IsUnderSkillSelect;
    }

    public IEnumerable<(StageDeck, sbyte)> PlayPrepare(GamePlayData data)
    {
        IEnumerable<(StageDeck, sbyte)> mainSkill = mainData.skillComponents
          .Where(y => { return PhaseCheck(y, SkillPhase.top) || PhaseCheck(y, SkillPhase.always); })
          .Select(y => { return y.PlayPrepare(data, this); });

        IEnumerable<(StageDeck, sbyte)> underSkill = underSkills
        .Where(y => { return PhaseCheck(y, SkillPhase.under) || PhaseCheck(y, SkillPhase.always); })
          .Select(y => { return y.PlayPrepare(data, this); });

        return mainSkill.Concat(underSkill);
    }

}
