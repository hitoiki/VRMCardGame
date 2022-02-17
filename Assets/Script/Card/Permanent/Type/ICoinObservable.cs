using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICoinObservable
{
    IObservable<(Coin key, int changeValue, int result)> GetObservableCoin();
}
