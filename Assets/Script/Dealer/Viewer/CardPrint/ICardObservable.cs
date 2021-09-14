using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public interface ICardObservable
{
    ReactiveProperty<Card> ObservableCard();
}
