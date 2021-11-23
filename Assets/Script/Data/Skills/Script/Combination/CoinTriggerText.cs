using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
[System.Serializable]
public class CoinTriggerText : ICoinProcess
{
    [SerializeField] private Coin ReactiveCoin;
    [SerializeField] private int threshold = 0;
    [SerializeReference, SubclassSelector] private IUseProcess useText;

    public void GetSkillProcess(CardFacade facade, Coin c, int n)
    {
        if (facade.sourceCoins[ReactiveCoin] >= threshold)
        {
            useText.GetSkillProcess(facade);
            facade.CoinToSource(c, -threshold);
        }
    }


    public bool GetIsSkillable(CardFacade facade, Coin coin, int n)
    {

        return ReactiveCoin == coin && facade.sourceCoins.ContainsKey(ReactiveCoin) && facade.sourceCoins[ReactiveCoin] >= threshold;

    }

    public string Text()
    {
        return ReactiveCoin.name + "が" + threshold.ToString() + "以上になった時、それを" + threshold.ToString() + "消費して" + useText.Text();
    }

    public string SkillName()
    {
        return ReactiveCoin.coinName + "Triggered[" + useText.SkillName() + "]";
    }

}
