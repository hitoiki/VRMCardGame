using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class LoadCoinSkill : ICoinProcess
{
    //物としての性質を与えるSkill
    [SerializeField] int drawAmount = 1;
    public IObservable<Unit> GetSkillProcess(CardFacade facade, Coin coin, int n)
    {
        return Observable.Defer<Unit>(() =>
        {
            if (facade.skillTarget.GetCoin()[facade.skillTarget.GetCardData().costCoin] >= facade.skillTarget.GetCardData().cost)
            {
                facade.skillTarget.BootOtherSkill(OtherSkillKind.OnPick, facade.skillQueue);
                facade.MoveCard(facade.skillTarget, DeckType.hands);
                facade.DeckDraw(DeckType.deck, DeckType.field, drawAmount);

            }
            return Observable.Empty<Unit>();
        });
    }

    public bool GetIsSkillable(CardFacade facade, Coin coin, int n)
    {
        return facade.skillTarget.GetCardData().costCoin == coin;
    }

    public string Text()
    {
        return "購入可能";
    }

    public string SkillName()
    {
        return "";
    }
}
