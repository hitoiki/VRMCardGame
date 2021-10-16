using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillComponent
{
    //CardDataはこれのListを持つ事にする
    //これはCoinEffectなどを持っていて、適宜それを取り出して色々する
    //引数を纏めたクラスで表現するのではなく、Skill側を纏める

    //効果を説明するあれ
    [SerializeField] ISkillDisPlay disPlay;
    [SerializeField] string text;
    //効果の纏まりなので、一個ずつ持つ
    [SerializeField] IUseSkill useSkill;
    [SerializeField] ICoinSkill coinSkill;
    [SerializeField] IDrawSkill drawSkill;
    [SerializeField] ISelectSkill selectSkill;

    public ISkillDisPlay GetDisplay()
    {
        return disPlay;
    }
    public string GetText()
    {
        return text;
    }

    public CardSkill GetUseSkill(Card source)
    {
        return useSkill.UseSkill(source);
    }
    public CardSkill GetCoinSkill(Card source, Coin c, short n)
    {
        return coinSkill.CoinSkill(source, c, n);
    }
    public CardSkill GetDrawSkill(Card source, StageDeck from, StageDeck to)
    {
        return drawSkill.DrawSkill(source, from, to);
    }
    public CardSkill GetSelectSkill(Card source, List<Card> targets)
    {
        return selectSkill.SelectSkill(source, targets);
    }
}