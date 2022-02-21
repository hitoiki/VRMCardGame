using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Context
{
    // カードが置かれている状況を示すクラス

    public CastContext cast;

    public Context(CastContext newCast)
    {
        cast = newCast;
    }
}
