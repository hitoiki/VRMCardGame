using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SkillPack
{
    //Skillを纏めるクラス

    [SerializeReference, SubclassSelector] private List<IUseProcess> useSkills;
    [SerializeReference, SubclassSelector] private List<ICoinProcess> coinSkills;
    [SerializeReference, SubclassSelector] private List<IDrawProcess> drawSkills;
    [SerializeReference, SubclassSelector] private List<OtherSkill> otherSkills;

    public SkillPack(List<IUseProcess> UseSkills, List<ICoinProcess> CoinSkills, List<IDrawProcess> DrawSkills, List<OtherSkill> OtherSkills)
    {
        this.useSkills = UseSkills;
        this.coinSkills = CoinSkills;
        this.drawSkills = DrawSkills;
        this.otherSkills = OtherSkills;
    }
    public string SkillText()
    {
        string skillTexts = "";
        if (useSkills.Any()) skillTexts += useSkills.Select(x => { return x.Text(); }).Aggregate((str1, str2) => str1 + str2);
        if (coinSkills.Any()) skillTexts += coinSkills.Select(x => { return x.Text(); }).Aggregate((str1, str2) => str1 + str2);
        if (drawSkills.Any()) skillTexts += drawSkills.Select(x => { return x.Text(); }).Aggregate((str1, str2) => str1 + str2);
        if (skillTexts == "")
        {
            Debug.Log("nullCardsText");
        }
        return skillTexts;
    }

    public List<Skill> UseSkill()
    {
        return useSkills.Select(y => { return y.GetSkill(); }).Where(x => { return x != null; })?.ToList();
    }

    public List<Skill> CoinSkill(Coin coin, int n)
    {
        return coinSkills.Select(y => { return y.GetSkill(coin, n); }).Where(x => { return x != null; })?.ToList();
    }

    public List<Skill> DrawSkill(IDeck from, IDeck to)
    {
        return drawSkills.Select(y => { return y.GetSkill(from, to); }).Where(x => { return x != null; })?.ToList();
    }

    public List<Skill> OtherSkill(OtherSkillKind kind)
    {
        return otherSkills.Select(y => { return y.GetSkill(kind); }).Where(x => { return x != null; })?.ToList();
    }

    public bool IsPlayable(CardFacade facade)
    {
        return useSkills.Aggregate(true, (b, skill) => { return b && skill.GetIsSkillable(facade); });
    }

    public static SkillPack Concat(SkillPack x, SkillPack y)
    {
        if (x == null) return y;
        if (y == null) return x;
        return new SkillPack(x.useSkills.Concat(y.useSkills).ToList(),
        x.coinSkills.Concat(y.coinSkills).ToList(),
        x.drawSkills.Concat(y.drawSkills).ToList(),
        x.otherSkills.Concat(y.otherSkills).ToList()
        );
    }


}
