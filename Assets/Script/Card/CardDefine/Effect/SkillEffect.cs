using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffect : MonoBehaviour
{
    // Skillによって生じるエフェクト効果を書くクラス
    // 必要な物は、処理に関わるカードのPrintableの位置と、それを用いるエフェクト効果
    //あと効果音とムード設定と…

    //このEffect自体をInstantiateして作る方向で

    public abstract void Effect(ICardPrintable sourse, ICardPrintable target);
}
