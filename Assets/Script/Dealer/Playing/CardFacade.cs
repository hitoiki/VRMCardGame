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
    public IPermanent skillTarget;
    public SkillUsingSubject skillsSubject => data.skillsSubject;
    public SkillQueue skillQueue => data.stage.queueObject;

    public int instantMoney
    {
        get { return data.player.instantMoney.Value; }
        set { data.player.instantMoney.Value = value; }
    }

    public int actionTimes
    {
        get { return data.player.actionTimes.Value; }
        set { data.player.actionTimes.Value = value; }
    }
    public CardFacade(FacadeData Data, IPermanent Source)
    {
        this.data = Data;
        this.skillTarget = Source;
    }
    //skillTargetを変えたfacadeを生成するやつ
    public CardFacade NewFacade(IPermanent newSource)
    {
        return new CardFacade(data, newSource);
    }
    //コスト支払い処理
    public virtual void CostPay()
    {
        foreach (IPermanent dealCard in DeckKey(DeckType.field))
        {
            dealCard.BootOtherSkill(OtherSkillKind.OnAction, skillQueue);
        };
    }
    //ターン終了時処理
    public virtual void TurnEnd()
    {
        foreach (IPermanent dealCard in DeckKey(DeckType.field))
        {
            dealCard.BootOtherSkill(OtherSkillKind.TurnEnd, skillQueue);
        };
        foreach (IPermanent dealCard in DeckKey(DeckType.hands))
        {
            dealCard.BootOtherSkill(OtherSkillKind.TurnEnd, skillQueue);
        };
        instantMoney = 0;
        actionTimes = 1;
        data.player.turn.Value += 1;
        if (data.player.turn.Value >= 11) data.player.GameEnd();
        Debug.Log("End");
        //Deckをリセット
        foreach (IPermanent p in data.stage.DeckKey(DeckType.hands))
        {
            p.MoveDeck(data.stage.DeckKey(DeckType.discard));
        }
        Draw(DeckType.deck, DeckType.hands, 5);

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
    public virtual void Draw(DeckType from, DeckType to, int n)
    {
        if (from == DeckType.deck && to == DeckType.hands && DeckKey(from).Count() <= n)
        {
            //Deckが足りない時に捨て札を追加する処理
            foreach (IPermanent permanent in DeckKey(DeckType.discard))
            {
                permanent.MoveDeck(DeckKey(DeckType.deck));
            }
        }
        IEnumerable<IPermanent> draws = DeckKey(from).Take(n);
        foreach (IPermanent permanent in draws)
        {
            permanent.MoveDeck(DeckKey(to));
        }
    }
}

