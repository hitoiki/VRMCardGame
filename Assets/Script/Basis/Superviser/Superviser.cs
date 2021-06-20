using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class Superviser : MonoBehaviour
{
    //SupervisedObjectの統合操作を行う
    //Instantiate,Update,Startはここを通して行う
    //Superviserを複数に分けて動かせても良いだろう。実装が複雑になるならSuperSuperViserも必要かも
    //適宜派生クラスを作り運用する事
    public List<SupervisedObject> follower = null;
    //SupervisedObjectに渡してよいデータ
    public SuperviserData data;

    private void Start()
    {

    }

    private void Update()
    {
        foreach (SupervisedObject f in follower)
        {
            f.SupervisedUpdate(this.data);
        }
    }

    public void Create(SupervisedObject obj, Vector3 pos, Quaternion quat)
    {
        SupervisedObject newobj = Instantiate<SupervisedObject>(obj, pos, quat);
        follower.Add(newobj);
        newobj.Init();
    }
}
[System.Serializable]
public class SuperviserData
{
    //俗に言うグローバル関数
    public bool Active = true;
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