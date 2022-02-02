using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
public interface IObservableDeck
{
    IObservable<CollectionReplaceEvent<ICard>> ReplaceEvent();
    IObservable<CollectionAddEvent<ICard>> AddEvent();
    IObservable<CollectionRemoveEvent<ICard>> RemoveEvent();
    List<ICard> Cards();
}
