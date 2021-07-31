using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class CursolCheck : MonoBehaviour, IGameState
{
    //Cardを手で動かすクラス
    //手で動かせる物を動かすだけ
    //動かした先がどんな場所かを判定して、返り値として返す

    //ドラッグして違和感のないように書いてある

    private IKeyPad key;
    [SerializeField] GameObject initKey;
    ICursolable[] cursolObj;
    Vector2 cursolPoint;
    bool bufClick = false;
    private void Awake()
    {
        //initKeyから取る
        key = initKey.GetComponent<IKeyPad>();
    }
    public void CrankIn()
    {
        key.LeftClick().Subscribe(x =>
        {
            //前回のクリック状況に合わせてイベントを呼び出す
            if (cursolObj != null)
            {
                if (!bufClick && x)
                {
                    foreach (ICursolable cursolable in cursolObj)
                    {
                        cursolable.Click(cursolPoint, ContactMode.Enter);
                    }
                }
                if (bufClick && !x)
                {
                    foreach (ICursolable cursolable in cursolObj)
                    {
                        cursolable.Click(cursolPoint, ContactMode.Exit);
                    }
                }
            }
            //クリック状態にあわせて色々保存
            if (!x) cursolObj = null;
            bufClick = x;
        });
        key.MousePoint().Subscribe(x =>
        {
            cursolPoint = x;
            Ray ray = Camera.main.ScreenPointToRay(x);
            RaycastHit hit_info = new RaycastHit();
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 0.01f, false);
            bool is_hit = Physics.Raycast(ray, out hit_info, 100f);
            //一度変数を介することで、クリックしている間はカーソルしている物を保持するようにする
            if (is_hit && (hit_info.collider.gameObject.GetComponents<ICursolable>() != null))
            {
                if (cursolObj == null) cursolObj = hit_info.collider.gameObject.GetComponents<ICursolable>();
            }
            else
            {
                if (!key.LeftClick().Value) cursolObj = null;
            }

        }
        );
    }
    //Update
    public void StateUpdate()
    {
        //Stay,CursolはUpdateなので
        if (cursolObj != null)
        {
            if (bufClick && key.LeftClick().Value)
            {
                foreach (ICursolable cursolable in cursolObj)
                {
                    cursolable.Click(cursolPoint, ContactMode.Stay);
                }
            }
            foreach (ICursolable cursolable in cursolObj)
            {
                cursolable.Cursol(cursolPoint);
            }
        }
    }
    //End
    public void CrankUp()
    {

    }

}
