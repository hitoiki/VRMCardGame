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
    [SerializeReference, SubclassSelector] IUseSkill useSkill;
    [SerializeReference, SubclassSelector] ICoinSkill coinSkill;
    [SerializeReference, SubclassSelector] IDrawSkill drawSkill;
    [SerializeField] SkillCondition condition;
    [Header("Effects")]
    [SerializeReference, SubclassSelector] ISkillEffect[] effect = new ISkillEffect[0];
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
        return string.Join("\n", str);
    }

    public Skill GetUseSkill()
    {
        if (useSkill != null) return new Skill(effect, useSkill.UseSkill());
        else return null;

    }
    public Skill GetCoinSkill(Coin c, int n)
    {
        if (coinSkill != null) return new Skill(effect, coinSkill?.CoinSkill(c, n));
        else return null;

    }
    public Skill GetDrawSkill(StageDeck from, StageDeck to)
    {
        if (drawSkill != null) return new Skill(effect, drawSkill.DrawSkill(from, to));
        else return null;
    }

    public SkillCondition GetCondition()
    {
        return condition;
    }

    public bool IsPlayable(Stage data)
    {
        if (useSkill == null) return true;
        return useSkill.UseAble(data);
    }
    public ICardChecking PlayPrepare(Stage data)
    {
        return useSkill.SelectCard(data);
    }
}