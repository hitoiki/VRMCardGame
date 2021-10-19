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
    public void AddCoin(CardDealer dealer, Coin c, short n)
    {
        if (coins.ContainsKey(c)) coins[c] += n;
        else coins.Add(c, n);
        CoinSkill(c, n).skill(dealer);
    }

    public void RemoveCoin(CardDealer dealer, Coin c, short n)
    {
        if (coins.ContainsKey(c))
        {
            if (coins[c] > n) coins[c] = 0;
            else coins[c] -= n;
        }
        CoinSkill(c, n).skill(dealer);
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

    private CardSkill SkillListRun(SkillDeal type)
    {
        //SkillTextから状況に応じてCardSkillを抽出する
        return new CardSkill(x =>
        {
            IEnumerable<CardSkill> mainSkill = mainData.skillComponents
            .Where(y => { return (PhaseCheck(y, SkillPhase.top) || PhaseCheck(y, SkillPhase.always)); }).Select(y => { return type(y); }).Where(x => { return x != null; })
           ;
            if (mainSkill != null)
            {
                foreach (CardSkill s in mainSkill)
                {
                    s.skill(x);
                }
            }
            IEnumerable<CardSkill> underSkill = underSkills.Where(y => { return (PhaseCheck(y, SkillPhase.under) || PhaseCheck(y, SkillPhase.always)); }).Select(y => { return type(y); }).Where(x => { return x != null; });
            if (underSkill != null)
            {
                foreach (CardSkill s in underSkill)
                {
                    s.skill(x);
                }
            }
        }
        );
    }

    public CardSkill CoinSkill(Coin coin, short n)
    {
        return SkillListRun(x => { return x.GetCoinSkill(this, coin, n); });
    }

    public CardSkill UseSkill()
    {
        return SkillListRun(x => { return x.GetUseSkill(this); });
    }

    public CardSkill DrawSkill(StageDeck from, StageDeck to)
    {
        return SkillListRun(x => { return x.GetDrawSkill(this, from, to); });
    }
    public CardSkill SelectSkill(List<Card> target)
    {
        return SkillListRun(x => { return x.GetSelectSkill(this, target); });
    }

    public bool IsPlayable(CardDealer dealer)
    {
        bool mainSkillPlayable = mainData.skillComponents
         .Where(y => { return PhaseCheck(y, SkillPhase.top) || PhaseCheck(y, SkillPhase.always); })
         .Aggregate(true, (b, skill) => { return b && skill.IsPlayable(dealer, this); });

        bool underSkillPlayable = underSkills
        .Where(y => { return PhaseCheck(y, SkillPhase.under) || PhaseCheck(y, SkillPhase.always); })
        .Aggregate(true, (b, skill) => { return b && skill.IsPlayable(dealer, this); });

        return mainSkillPlayable && underSkillPlayable;
    }

    public bool IsSelect()
    {
        bool IsMainSkillSelect = mainData.skillComponents.Aggregate(true, (b, skill) => { return b && skill.IsSelect(); });
        bool IsUnderSkillSelect = underSkills.Aggregate(true, (b, skill) => { return b && skill.IsSelect(); });
        return IsMainSkillSelect && IsUnderSkillSelect;
    }

    public IEnumerable<(StageDeck, sbyte)> PlayPrepare(CardDealer dealer)
    {
        IEnumerable<(StageDeck, sbyte)> mainSkill = mainData.skillComponents
          .Where(y => { return PhaseCheck(y, SkillPhase.top) || PhaseCheck(y, SkillPhase.always); })
          .Select(y => { return y.PlayPrepare(dealer, this); });

        IEnumerable<(StageDeck, sbyte)> underSkill = underSkills
        .Where(y => { return PhaseCheck(y, SkillPhase.under) || PhaseCheck(y, SkillPhase.always); })
          .Select(y => { return y.PlayPrepare(dealer, this); });

        return mainSkill.Concat(underSkill);
    }

}
