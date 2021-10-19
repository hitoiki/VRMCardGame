using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "CardComponent")]
public class SkillComponent : ScriptableObject
{
    //CardDataはこれのListを持つ事にする
    //これはCoinEffectなどを持っていて、適宜それを取り出して色々する
    //引数を纏めたクラスで表現するのではなく、Skill側を纏める

    //効果を説明するあれ
    [SerializeField] GameObject initDisPlay;
    [SerializeField] ISkillDisPlay disPlay => initDisPlay.GetComponent<ISkillDisPlay>();
    //効果の纏まりなので、一Componentに一つ持つ
    [Header("Skill")]
    [SerializeField] ScriptableUseSkill useSkill;
    [SerializeField] ScriptableCoinSkill coinSkill;
    [SerializeField] ScriptableDrawSkill drawSkill;
    [SerializeField] ScriptableSelectSkill selectSkill;
    [SerializeField] SkillCondition condition;

    public ISkillDisPlay GetDisplay()
    {
        return disPlay;
    }
    public string GetText()
    {
        List<string> str = new List<string>();
        if (useSkill != null) str.Add(useSkill.Text());
        if (coinSkill != null) str.Add(coinSkill.Text());
        if (drawSkill != null) str.Add(drawSkill.Text());
        if (selectSkill != null) str.Add(selectSkill.Text());
        return string.Join("\n", str);
    }

    public CardSkill GetUseSkill(Card source)
    {
        return useSkill?.UseSkill(source);
    }
    public CardSkill GetCoinSkill(Card source, Coin c, short n)
    {
        return coinSkill?.CoinSkill(source, c, n);
    }
    public CardSkill GetDrawSkill(Card source, StageDeck from, StageDeck to)
    {
        return drawSkill?.DrawSkill(source, from, to);
    }
    public CardSkill GetSelectSkill(Card source, List<Card> targets)
    {
        return selectSkill?.SelectSkill(source, targets);
    }

    public SkillCondition GetCondition()
    {
        return condition;
    }

    public bool IsPlayable(CardDealer dealer, Card source)
    {
        if (useSkill == null) return true;
        return useSkill.UseAble(dealer, source);
    }

    public bool IsSelect()
    {
        return selectSkill != null;
    }

    public (StageDeck, sbyte) PlayPrepare(CardDealer dealer, Card source)
    {
        return selectSkill.SelectCard(dealer, source);
    }
}