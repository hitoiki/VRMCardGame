using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class EnemyCoinSkill : ICoinProcess
{
    //敵としての性質を与えるSkill
    [SerializeField] Coin costCoin;
    [SerializeField] int cost;
    [SerializeField] Pack addPack;
    public IObservable<Unit> GetSkillProcess(CardFacade facade, Coin coin, int n)
    {
        return Observable.Defer<Unit>(() =>
        {
            if (facade.skillTarget.GetCoin()[costCoin] >= cost)
            {
                facade.skillTarget.BootOtherSkill(OtherSkillKind.OnPick);
                facade.AddPack(addPack, DeckType.deck);
            }
            return Observable.Empty<Unit>();
        });
    }

    public bool GetIsSkillable(CardFacade facade, Coin coin, int n)
    {
        return costCoin == coin;
    }

    public string Text()
    {
        return "撃破報酬:" + addPack.textName;
    }

    public string SkillName()
    {
        return "Enemy";
    }
}