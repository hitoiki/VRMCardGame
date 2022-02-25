using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
public class IfPickingRawSkill : ISkillProcessPermanent
{
    //一つカードを選択して、それを対象にRawSkillを発動する
    [SerializeReference, SubclassSelector] IRawSkill rawSkill;
    [SerializeReference, SubclassSelector] ISkillCardBool cardCondition;
    public IObservable<Unit> GetSkillProcess(CardFacade facade, IPermanent card)
    {
        return Observable.Defer<Unit>(() =>
        {
            return rawSkill.GetSkillProcess(facade);
        });

    }
    public bool GetIsSkillable(CardFacade facade, IPermanent card)
    {
        return cardCondition.SkillBool(card);
    }
    public string Text()
    {
        return "カードが取得されたとき、それが" + cardCondition.Text() + "なら、" + rawSkill.Text();
    }

    public string SkillName()
    {
        return "IfPicking" + rawSkill.SkillName() + "," + cardCondition.SkillName();
    }
}
