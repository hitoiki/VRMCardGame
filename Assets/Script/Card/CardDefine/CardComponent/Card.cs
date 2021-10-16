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
    private List<SkillComponent> mainSkills => mainData.skillTexts;
    public List<CardData> underCards = new List<CardData>();
    private List<SkillComponent> underSkills => underCards.SelectMany(x => { return x.skillTexts; }).ToList();
    public ReactiveDictionary<Coin, short> coins = new ReactiveDictionary<Coin, short>();

    delegate CardSkill SkillDeal(SkillComponent text);

    public Card(CardData d)
    {
        mainData = d;
    }
    //効果が反応するようなCoin操作
    public void AddCoin(CardDealer dealer, Coin c, short n)
    {
        CoinSkill(SkillPriority.beforeCoin, c, n).skill(dealer);
        if (coins.ContainsKey(c)) coins[c] += n;
        else coins.Add(c, n);
        CoinSkill(SkillPriority.afterCoin, c, n).skill(dealer);
    }

    public void RemoveCoin(CardDealer dealer, Coin c, short n)
    {
        CoinSkill(SkillPriority.beforeCoin, c, n).skill(dealer);
        if (coins.ContainsKey(c))
        {
            if (coins[c] > n) coins[c] = 0;
            else coins[c] -= n;
        }
        CoinSkill(SkillPriority.afterCoin, c, n).skill(dealer);
    }

    public string CardText()
    {
        string str = "";
        str += mainData.cardName + "「" + mainData.CardText() + "」\n";
        foreach (CardData data in underCards) str += data.cardName + "「" + mainData.CardText() + "」\n";
        return str;
    }

    //CardDetaの"Text"を読み取って効果を発動する
    //CoinはCoinの変更時に
    //Dealerとかで発動タイミングの統括を図ったほうが良いような感じもする
    //非同期処理でないはずなので多分大丈夫きっと恐らく

    private CardSkill SkillListRun(SkillDeal type, SkillPriority priority)
    {
        //SkillTextから状況に応じてCardSkillを抽出する
        return new CardSkill(priority, SkillPhase.Composite, x =>
          {
              IEnumerable<CardSkill> mainSkill = mainData.skillTexts.Select(y => { return type(y); })
              .Where(y => { return y.priority == priority && ((y.activePhase == SkillPhase.top) || (y.activePhase == SkillPhase.always)); });
              if (mainSkill != null)
              {
                  foreach (CardSkill s in mainSkill)
                  {
                      s.skill(x);
                  }
              }
              IEnumerable<CardSkill> underSkill = underSkills.Select(y => { return type(y); })
              .Where(y => { return y.priority == priority && ((y.activePhase == SkillPhase.top) || (y.activePhase == SkillPhase.always)); });
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

    public CardSkill CoinSkill(SkillPriority priority, Coin coin, short n)
    {
        return SkillListRun(x => { return x.GetCoinSkill(this, coin, n); }, priority);
    }

    public CardSkill UseSkill(SkillPriority priority)
    {
        return SkillListRun(x => { return x.GetUseSkill(this); }, priority);
    }

    public CardSkill DrawSkill(SkillPriority priority, StageDeck from, StageDeck to)
    {
        return SkillListRun(x => { return x.GetDrawSkill(this, from, to); }, priority);
    }
    public CardSkill SelectSkill(SkillPriority priority, List<Card> target)
    {
        return SkillListRun(x => { return x.GetSelectSkill(this, target); }, priority);
    }

}
