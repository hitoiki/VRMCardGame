using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

public class StateDealer : MonoBehaviour
{
    //インターン先の人曰く、状態のクラスを作ってそれ毎に処理を書くと良いとの事
    //IStateを継承して、それを取り扱う仕組みにする
    [SerializeField] public List<GameStateSet> states;
    private GameStateSet loadingState;
    private ReactiveProperty<string> _stateName = new ReactiveProperty<string>();
    public IReadOnlyReactiveProperty<string> stateName => _stateName;

    private void Start()
    {
        loadingState = states.First();
        loadingState.CrankIn();
    }

    private void Update()
    {
        loadingState.StateUpdate();
    }

    public void ChangeState(string nextState)
    {
        if (states.First(x => { return x.stateName == nextState; }) == null) return;
        if (nextState == loadingState.stateName)
        {
            Debug.Log("Have Loaded");
            return;
        }
        loadingState.CrankUp();
        loadingState = states.First(x => { return x.stateName == nextState; });
        loadingState.CrankIn();
        _stateName.Value = nextState;
        Debug.Log("NowState" + nextState);

    }

}
