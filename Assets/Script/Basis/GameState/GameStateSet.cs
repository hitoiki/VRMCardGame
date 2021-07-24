using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UniRx;
using System.Linq;

public class GameStateSet : MonoBehaviour
{
    //Stateを纏めるやつ
    //こういうのが合った方が便利なため
    //再起的な取得を防ぐためIGameStateは外してる
    [SerializeField] public string stateName;
    [SerializeField] public List<GameObject> Initforrower = new List<GameObject>(1);
    //なんかしっくりこないが取り敢えずInspectorからこれで受け取る
    public List<IGameState> follower = null;

    //Inspectorでアタッチされる度に呼び出される関数で、型があってなかったら消している
    private void OnValidate()
    {
        if (Initforrower != null) Initforrower = Initforrower.Where(x => { return (x == null) || (x.GetComponent<IGameState>() != null); }).ToList();
    }
    //ListにgameObjectを取ってそこからIGameStateを抽出している
    private void Start()
    {
        follower = Initforrower.SelectMany(x => { return x.GetComponents<IGameState>(); }).ToList();
    }

    public void CrankIn()
    {

        foreach (IGameState f in follower)
        {
            f.CrankIn();
        }
    }

    public void StateUpdate()
    {
        foreach (IGameState f in follower)
        {
            f.StateUpdate();
        }
    }

    public void CrankUp()
    {
        foreach (IGameState f in follower)
        {
            f.CrankUp();
        }
    }
}
