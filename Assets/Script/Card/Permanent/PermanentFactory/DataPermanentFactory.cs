using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPermanentFactory : IPermanentFactory
{
    [SerializeField] private Context context;
    public IPermanent CardMake(ICard card, IDeck deck)
    {
        return new DataPermanent(card, deck, context);
    }
}
