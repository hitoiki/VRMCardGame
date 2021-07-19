using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class GameState : MonoBehaviour
{
    //インターン先の人曰く、状態のクラスを作ってそれ毎に処理を書くと良いとの事
    //IStateを継承して、それを取り扱う仕組みにする
    public List<IGameStated> follower = null;

    private void Start()
    {

    }

    private void Update()
    {
        foreach (IGameStated f in follower)
        {
            f.StateUpdate();
        }
    }
}
/*
接触処理で毎回迷ってるのでどうにかする
片方が片方のInterFaseを読み込む方式
この時片方が片方を生み出す仕組みとなると、
A→B(interface呼ばれる)→C(生成クラス)→Aと循環してしまう

B→A(interface呼ばれる)
B→C→A
なら問題はないので出来ればこれで
まあ忌避しすぎも本末転倒なので考えすぎないよう

*/