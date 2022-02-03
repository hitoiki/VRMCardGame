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
    public SkillQueueObject skillQueue => data.stage.queueObject;
    public CardFacade(FacadeData Data, ICard Source)
    {
        this.data = Data;
        this.skillTarget = Source;
    }
    //あんま良くない気がするけど暫定これで
    public CardFacade NewFacade(ICard newSource)
    {
        return new CardFacade(data, newSource);
    }
    public virtual void CostPay()
    {
        foreach (ICard dealCard in data.fieldFactory.GetCards().Select(x => { return x.GetCard(); }))
        {
            dealCard.BootOtherSkill(OtherSkillKind.OnAction, skillQueue);
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
    //
    public void DrawMove(DeckType from, DeckType to, int n)
    {
        DeckKey(to).Add(DeckKey(from).Draw(n));
    }
}

