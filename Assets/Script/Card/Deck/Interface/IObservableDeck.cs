using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
public interface IObservableDeck
{
    IObservable<CollectionReplaceEvent<ICard>> ObservableReplace();
    IObservable<CollectionAddEvent<ICard>> ObservableAdd();
    IObservable<CollectionRemoveEvent<ICard>> ObservableRemove();
    List<ICard> Cards();
}
