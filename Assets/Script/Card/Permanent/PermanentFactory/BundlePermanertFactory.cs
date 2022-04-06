using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundlePermanertFactory : IPermanentFactory
{
    [SerializeField] private Stage stage;
    [SerializeField] private Context context;
    [SerializeField] private int amount;
    public IPermanent CardMake(ICard card, IDeck deck)
    {
        return new BundlePermanent(card, deck, context, stage.queueObject, amount);
    }
}

