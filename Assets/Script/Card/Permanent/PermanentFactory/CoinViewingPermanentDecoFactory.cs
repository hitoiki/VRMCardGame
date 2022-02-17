using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinViewingPermanentDecoFactory : IPermanentFactory
{
    [SerializeReference, SubclassSelector] private IPermanentFactory factory;
    public IPermanent CardMake(ICard card, IDeck deck)
    {
        return new CoinViewingPermanentDeco(factory.CardMake(card, deck));
    }
}
