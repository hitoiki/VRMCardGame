using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class SupervisedObject : MonoBehaviour
{
    //これから書く全てのオブジェクトはこれを継承する
    //・ゲーム状況でUpdateが動くか否かを制御したい
    //・その他帯域的に参照するクラスを纏めたい
    //・次いでにFlyWeightも出来るとよい

    //基本的にSuperviserが大量にこのクラスを保有、管轄する
    //通常のUpdate、Startは書かない事

    //Start、及び生成時に呼び出される
    public virtual void Init() { }
    //Update。
    public virtual void SupervisedUpdate(SuperviserData data) { }

}
