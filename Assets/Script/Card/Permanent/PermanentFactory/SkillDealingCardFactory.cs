using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillDealingCardFactory : IPermanentFactory
{
    [SerializeField] private Stage stage;
    [SerializeField] private Context context;
    public IPermanent CardMake(ICard card, IDeck deck)
    {
        return new SkillDealingPermanent(card, deck, context, stage.queueObject);
    }
}
