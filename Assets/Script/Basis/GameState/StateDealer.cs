using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StateDealer : MonoBehaviour
{
    //インターン先の人曰く、状態のクラスを作ってそれ毎に処理を書くと良いとの事
    //IStateを継承して、それを取り扱う仕組みにする
    [SerializeField] public List<GameStateSet> states;
    private GameStateSet loadingState;
    [SerializeField] public bool isPose = false;

    private void Start()
    {
        loadingState = states.First();
        loadingState.CrankIn();
    }

    private void Update()
    {
        if (!isPose) loadingState.StateUpdate();
    }

    public void StateSwitch(string nextState)
    {
        if (states.First(x => { return x.stateName == nextState; }) != null)
        {
            loadingState.CrankUp();
            loadingState = states.First(x => { return x.stateName == nextState; });
            loadingState.CrankIn();
        }
    }

}
