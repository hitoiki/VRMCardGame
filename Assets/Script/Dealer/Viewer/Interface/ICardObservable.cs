using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public interface ICardObservable
{
    IReadOnlyReactiveProperty<Card> ObservableCard();
}
