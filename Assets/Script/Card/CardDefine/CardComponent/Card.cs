using System;
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
    private readonly ReactiveDictionary<Coin, short> _coins = new ReactiveDictionary<Coin, short>();

    //中身の値だけを公開するためのList(このListの値を変えてもReactiveCollection側は変わらない)
    public Dictionary<Coin, short> coins => _coins.ToDictionary(pair => pair.Key, pair => pair.Value);
    //ReactiveCollectionのうちIObservableだけを公開し、処理を登録できるように
    public IObservable<DictionaryReplaceEvent<Coin, short>> CoinsReplace => _coins.ObserveReplace();
    public IObservable<DictionaryAddEvent<Coin, short>> CoinsAdd => _coins.ObserveAdd();
    public IObservable<DictionaryRemoveEvent<Coin, short>> CoinsRemove => _coins.ObserveRemove();

    delegate Skill SkillDeal(SkillPack text);

    public Card(CardData d)
    {
        mainData = d;
    }
    public void AddCoin(CardFacade facade, Coin c, short n)
    {
        if (_coins.ContainsKey(c)) _coins[c] += n;
        else _coins.Add(c, n);
        Debug.Log(_coins[c]);
        //facade.skillQueue.Push(this.CoinSkill(c, n));
    }

    public void RemoveCoin(CardFacade facade, Coin c, short n)
    {
        if (_coins.ContainsKey(c))
        {
            if (_coins[c] > n) _coins[c] = 0;
            else _coins[c] -= n;
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

    public List<Skill> CoinSkill(Coin coin, short n)
    {
        return SkillListRun(x => { return x.GetCoinSkill(this, coin, n); });
    }

    public List<Skill> UseSkill()
    {
        return SkillListRun(x => { return x.GetUseSkill(this); });
    }

    public List<Skill> DrawSkill(StageDeck from, StageDeck to)
    {
        return SkillListRun(x => { return x.GetDrawSkill(this, from, to); });
    }
    public List<Skill> SelectSkill(List<Card> target)
    {
        return SkillListRun(x => { return x.GetSelectSkill(this, target); });
    }

    public bool IsPlayable(GamePlayData data)
    {
        bool mainSkillPlayable = mainData.skillPack
         .Where(y => { return PhaseCheck(y, SkillPhase.top) || PhaseCheck(y, SkillPhase.always); })
         .Aggregate(true, (b, skill) => { return b && skill.IsPlayable(data, this); });

        bool underSkillPlayable = underSkills
        .Where(y => { return PhaseCheck(y, SkillPhase.under) || PhaseCheck(y, SkillPhase.always); })
        .Aggregate(true, (b, skill) => { return b && skill.IsPlayable(data, this); });

        return mainSkillPlayable && underSkillPlayable;
    }

    public bool IsSelect(GamePlayData data)
    {
        bool IsMainSkillSelect = mainData.skillPack.Aggregate(true, (b, skill) => { return b && skill.IsSelect(data); });
        bool IsUnderSkillSelect = underSkills.Aggregate(true, (b, skill) => { return b && skill.IsSelect(data); });
        return IsMainSkillSelect && IsUnderSkillSelect;
    }

    public List<(StageDeck, sbyte)> PlayPrepare(GamePlayData data)
    {
        IEnumerable<(StageDeck, sbyte)> mainSkill = mainData.skillPack
          .Where(y => { return PhaseCheck(y, SkillPhase.top) || PhaseCheck(y, SkillPhase.always); })
          .Select(y => { return y.PlayPrepare(data, this); });

        IEnumerable<(StageDeck, sbyte)> underSkill = underSkills
        .Where(y => { return PhaseCheck(y, SkillPhase.under) || PhaseCheck(y, SkillPhase.always); })
          .Select(y => { return y.PlayPrepare(data, this); }).ToList();

        return mainSkill.Concat(underSkill).ToList();
    }

}
