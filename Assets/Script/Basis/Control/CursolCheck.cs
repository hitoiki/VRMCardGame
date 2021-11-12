using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class CursolCheck : MonoBehaviour
{
    //Cardを手で動かすクラス
    //手で動かせる物を動かすだけ
    //動かした先がどんな場所かを判定して、返り値として返す

    //ドラッグして違和感のないように書いてある
    //カーソルは動かしてないけど射線に物が入って来た時は呼ばれない

    private IKeyPad key;
    [SerializeField] GameObject initKey = null;
    ICursolable[] cursolObj;
    Vector2 cursolPoint;
    bool bufClick = false;
    bool isEnterCalled = false;

    IDisposable _leftClick;
    IDisposable _mousePoint;
    private void Awake()
    {
        //initKeyから取る
        key = initKey.GetComponent<IKeyPad>();
        _leftClick = key.LeftClick().Subscribe(x =>
        {
            //前回のクリック状況に合わせてイベントを呼び出す
            //カーソルしてる物があり、前回クリックしていない
            if (cursolObj != null && !bufClick && x && !isEnterCalled)
            {
                foreach (ICursolable cursolable in cursolObj)
                {
                    cursolable.Click(cursolPoint, ContactMode.Enter);
                }
                isEnterCalled = true;
            }
            //カーソルしてる物があり、Enterが呼ばれていて、クリックが離れた
            if (cursolObj != null && isEnterCalled && bufClick && !x)
            {
                foreach (ICursolable cursolable in cursolObj)
                {
                    cursolable.Click(cursolPoint, ContactMode.Exit);
                }
            }
            //クリックしていないなら、カーソルを離す
            if (!x) cursolObj = null;
            //何もカーソルしてないか、クリックされていないならEnter履歴を消す
            if (cursolObj == null)
            {
                isEnterCalled = false;
            }
            //前回のクリック状態を保存
            bufClick = x;
        });
    }
    //Update
    public void Update()
    {
        //冷静に考えてみればカーソル以外のオブジェクトが動いてくる事もあるからUpdateで処理
        //Stateで止まらないのがちょっと不安
        cursolPoint = key.MousePoint().Value;
        Ray ray = Camera.main.ScreenPointToRay(cursolPoint);
        RaycastHit hit_info = new RaycastHit();
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 0.01f, false);
        bool is_hit = Physics.Raycast(ray, out hit_info, 100f);
        //一度変数を介することで、クリックしている間はカーソルしている物を保持するようにする
        if (is_hit && (hit_info.collider.gameObject.GetComponents<ICursolable>() != null))
        {
            if (cursolObj == null) cursolObj = hit_info.collider.gameObject.GetComponents<ICursolable>();
            foreach (ICursolable cursolable in cursolObj)
            {
                cursolable.Cursol(cursolPoint, ContactMode.Enter);
            }
        }
        else
        {

            if (!isEnterCalled)
            {
                if (cursolObj != null && cursolObj.Any())
                {
                    foreach (ICursolable cursolable in cursolObj)
                    {
                        cursolable.Cursol(cursolPoint, ContactMode.Exit);
                    }
                }
                cursolObj = null;
            }
        }
        //Stay,CursolはUpdateなので
        //なんもないなら何もしない
        if (cursolObj == null) return;
        if (bufClick && isEnterCalled)
        {
            foreach (ICursolable cursolable in cursolObj)
            {
                cursolable.Click(cursolPoint, ContactMode.Stay);
            }
        }
        foreach (ICursolable cursolable in cursolObj)
        {
            cursolable.Cursol(cursolPoint, ContactMode.Stay);
        }

    }

}
