using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class VRMCardText : MonoBehaviour, IGameState
{
    [SerializeField] private GameObject initVRM;
    private ICardObservable vrm;
    [SerializeField] private Text SkillText;
    [SerializeField] private Scrollbar bar;

    private IDisposable _Vrm;

    private void OnValidate()
    {
        if (initVRM?.GetComponent<ICardObservable>() == null) initVRM = null;
    }

    private void Start()
    {
        vrm = initVRM?.GetComponent<ICardObservable>();
    }

    public void CrankIn()
    {
        _Vrm = vrm.ObservableCard().Where(x => { return x != null; }).Subscribe(x =>
            {
                Print(x.GetCardData());
            });

    }

    public void StateUpdate()
    {

    }
    //End
    public void CrankUp()
    {
        //購読停止
        _Vrm.Dispose();
    }

    public void Print(CardData card)
    {
        SkillText.text = card.CardText();
        bar.value = 1;
    }

}
