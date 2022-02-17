using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillDealingCardFactory : IPermanentFactory
{
    [SerializeField] private Stage stage;
    public IPermanent CardMake(ICard card, IDeck deck)
    {
        return new SkillDealingPermanent(card, deck, stage.queueObject);
    }
}
