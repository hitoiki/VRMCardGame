using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

public class CardFacade
{
    //Cardが出来る処理を書く   
    //主にICardの取得処理を担当する
    FacadeData data;
    public ICard skillTarget;
    public SkillUsingSubject skillsSubject => data.skillsSubject;
    public SkillQueue skillQueue => data.stage.queueObject;
    public CardFacade(FacadeData Data, ICard Source)
    {
        this.data = Data;
        this.skillTarget = Source;
    }
    //skillTargetを変えたfacadeを生成するやつ
    public CardFacade NewFacade(ICard newSource)
    {
        return new CardFacade(data, newSource);
    }
    //コスト支払い処理
    public virtual void CostPay()
    {
        foreach (ICard dealCard in DeckKey(DeckType.field))
        {
            dealCard.BootOtherSkill(OtherSkillKind.OnAction, skillQueue);
        };
    }
    //ターン終了時処理
    public virtual void TurnEnd()
    {
        foreach (ICard dealCard in DeckKey(DeckType.field))
        {
            dealCard.BootOtherSkill(OtherSkillKind.TurnEnd, skillQueue);
        };
        foreach (ICard dealCard in DeckKey(DeckType.hands))
        {
            dealCard.BootOtherSkill(OtherSkillKind.TurnEnd, skillQueue);
        };
    }
    //条件を満たすカードのリストを渡す
    public IDeck DeckKey(DeckType type)
    {
        return data.stage.DeckKey(type);
    }
    //Playerに対する効果
    //Damege
    public void PlayerDamage(int damage)
    {
        data.player.Damage(damage);
    }
    //Deckから引いてくる効果
    public void DrawMove(DeckType from, DeckType to, int n)
    {
        DeckKey(to).Add(DeckKey(from).Draw(n));
    }
}

