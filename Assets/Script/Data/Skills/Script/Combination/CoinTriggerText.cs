using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
[System.Serializable]
public class CoinTriggerText : ICoinSkill
{
    [SerializeField] private Coin ReactiveCoin;
    [SerializeField] private int threshold = 0;
    [SerializeReference, SubclassSelector] private IUseSkill useText;

    private void Skill(CardFacade facade, Coin c, int n)
    {
        if (facade.sourceCoins[ReactiveCoin] >= threshold)
        {
            useText.UseSkill().skill(facade);
            facade.CoinToSource(c, -threshold);
        }
    }
    public SkillProcess CoinSkill(Coin coin, int n)
    {
        return new SkillProcess(
        (CardFacade dealer) => { Skill(dealer, coin, n); }
        );
    }

    public string Text()
    {
        return ReactiveCoin.name + "が" + threshold.ToString() + "以上になった時、" + useText.Text();
    }

}
