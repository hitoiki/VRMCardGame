using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SkillPack
{
    //Skillを纏めるクラス

    [SerializeReference, SubclassSelector] private List<ISkillProcessTag> skills;

    public SkillPack(List<ISkillProcessTag> newSkills)
    {
        this.skills = newSkills;
    }
    public string SkillText()
    {
        string skillTexts = "";
        if (skills.Any()) skillTexts += skills.Select(x => { return x.Text(); }).Aggregate((str1, str2) => str1 + str2);
        if (skillTexts == "")
        {
            Debug.Log("nullCardsText");
        }
        return skillTexts;
    }

    public List<Skill> UseProcess()
    {
        return skills.OfType<ISkillProcessUse>().Select(y => { return y?.GetSkill(); }).Where(x => { return x != null; })?.ToList();
    }
    public List<ISkillProcessCheck> CheckProcess()
    {
        return skills.OfType<ISkillProcessCheck>().Where(x => { return x != null; })?.ToList();
    }
    public List<Skill> SkillProcess<T>(T t)
    {
        return skills.OfType<ISkillProcess<T>>().Select(y => { return y?.GetSkill(t); }).Where(x => { return x != null; })?.ToList();
    }
    public static SkillPack Concat(SkillPack x, SkillPack y)
    {
        if (x == null) return y;
        if (y == null) return x;
        return new SkillPack(x.skills.Concat(y.skills).ToList());
    }


}
