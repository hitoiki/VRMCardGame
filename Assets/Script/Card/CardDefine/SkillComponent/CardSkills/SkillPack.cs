using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "CardComponent")]
public class SkillPack : ScriptableObject
{
    //CardDataはこれのListを持つ事にする
    //これはCoinEffectなどを持っていて、適宜それを取り出して色々する
    //引数を纏めたクラスで表現するのではなく、Skill側を纏める

    //効果を説明するあれ
    [SerializeField] GameObject initDisPlay;
    [SerializeField] ISkillDisPlay disPlay => initDisPlay.GetComponent<ISkillDisPlay>();
    [SerializeField] string skillText;
    //効果の纏まりなので、一Componentに一つ持つ
    [Header("Skill")]
    [SerializeField] ScriptableUseSkill useSkill;
    [SerializeField] ScriptableCoinSkill coinSkill;
    [SerializeField] ScriptableDrawSkill drawSkill;
    [SerializeField] ScriptableSelectSkill selectSkill;
    [SerializeField] SkillCondition condition;
    [Header("Effect")]
    [SerializeField] SkillEffect effect;

    public ISkillDisPlay GetDisplay()
    {
        return disPlay;
    }
    public string GetText()
    {
        //疾走、等キーワード能力ならその説明を返し、そうでないなら厳密に返す
        if (skillText != "") return skillText;
        else return GetStrictText();
    }

    public string GetStrictText()
    {
        List<string> str = new List<string>();
        if (useSkill != null) str.Add(useSkill.Text());
        if (coinSkill != null) str.Add(coinSkill.Text());
        if (drawSkill != null) str.Add(drawSkill.Text());
        if (selectSkill != null) str.Add(selectSkill.Text());
        return string.Join("\n", str);
    }

    public Skill GetUseSkill(Card source)
    {
        if (useSkill != null) return new Skill(source, effect, useSkill.UseSkill(source));
        else return null;

    }
    public Skill GetCoinSkill(Card source, Coin c, short n)
    {
        if (coinSkill != null) return new Skill(source, effect, coinSkill?.CoinSkill(source, c, n));
        else return null;

    }
    public Skill GetDrawSkill(Card source, StageDeck from, StageDeck to)
    {
        if (drawSkill != null) return new Skill(source, effect, drawSkill.DrawSkill(source, from, to));
        else return null;
    }
    public Skill GetSelectSkill(Card source, List<Card> targets)
    {
        if (selectSkill != null) return new Skill(source, effect, selectSkill.SelectSkill(source, targets));
        else return null;
    }

    public SkillCondition GetCondition()
    {
        return condition;
    }

    public bool IsPlayable(GamePlayData data, Card source)
    {
        if (useSkill == null) return true;
        return useSkill.UseAble(data, source);
    }

    public bool IsSelect(GamePlayData data)
    {
        return selectSkill != null;
    }

    public (StageDeck, sbyte) PlayPrepare(GamePlayData data, Card source)
    {
        return selectSkill.SelectCard(data, source);
    }
}