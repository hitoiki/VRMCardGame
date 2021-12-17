using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

[System.Serializable]
public class EffectRaw
{
    // Effectの処理を全てのコードに書くのは流石に非効率
    // Effect登録、発行を行うクラスをここに書く

    [SerializeReference, SubclassSelector] ISkillEffect[] effects;

    public void EffectBoot()
    {


    }
}
