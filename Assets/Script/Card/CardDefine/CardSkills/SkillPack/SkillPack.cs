using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SkillPack
{
    //Skillを纏めるクラス

    [SerializeField] public List<UseSkill> useSkills;
    [SerializeField] public List<CoinSkill> coinSkills;
    [SerializeField] public List<DrawSkill> drawSkills;

    public SkillPack(List<UseSkill> UseSkills, List<CoinSkill> CoinSkills, List<DrawSkill> DrawSkills)
    {
        this.useSkills = UseSkills;
        this.coinSkills = CoinSkills;
        this.drawSkills = DrawSkills;
    }
    public string SkillText()
    {
        IEnumerable<ISkillText> skillTexts = useSkills.Select(x => { return x.useSkill as ISkillText; });
        skillTexts.Concat(coinSkills.Select(x => { return x as ISkillText; }));
        skillTexts.Concat(drawSkills.Select(x => { return x as ISkillText; }));
        if (!skillTexts.Any())
        {
            Debug.Log("nullCardsText");
            return "";
        }
        return skillTexts.Select(x => { return x.Text(); }).Aggregate((str1, str2) => str1 + str2);
    }

    public List<Skill> UseSkill()
    {
        return useSkills.Select(y => { return y.GetSkill(); }).Where(x => { return x != null; }).ToList();
    }

    public List<Skill> CoinSkill(Coin coin, int n)
    {
        return coinSkills.Select(y => { return y.GetSkill(coin, n); }).Where(x => { return x != null; }).ToList();
    }

    public List<Skill> DrawSkill(StageDeck from, StageDeck to)
    {
        return drawSkills.Select(y => { return y.GetSkill(from, to); }).Where(x => { return x != null; }).ToList();
    }

    public bool IsPlayable(Stage data)
    {
        return useSkills.Aggregate(true, (b, skill) => { return b && skill.useSkill.UseAble(data); });
    }

    public List<ICardChecking> PlayPrepare(Stage data)
    {
        return useSkills.Select(y => { return y.useSkill.PlayPrepare(data); }).ToList();
    }


    public static SkillPack Concat(SkillPack x, SkillPack y)
    {
        if (x == null) return y;
        if (y == null) return x;
        return new SkillPack(x.useSkills.Concat(y.useSkills).ToList(),
        x.coinSkills.Concat(y.coinSkills).ToList(),
        x.drawSkills.Concat(y.drawSkills).ToList());
    }


}
