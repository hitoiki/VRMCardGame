using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinViewingPermanentFactory : IPermanentFactory
{
    [SerializeField] private Stage stage;
    [SerializeField] private Context context;
    public IPermanent CardMake(ICard card, IDeck deck)
    {
        return new CoinViewingPermanent(card, deck, context, stage.queueObject);
    }
}
