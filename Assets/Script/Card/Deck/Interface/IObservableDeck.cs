using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
public interface IObservableDeck
{
    IObservable<CollectionReplaceEvent<IPermanent>> ReplaceEvent();
    IObservable<CollectionAddEvent<IPermanent>> AddEvent();
    IObservable<CollectionRemoveEvent<IPermanent>> RemoveEvent();
}
