using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayRunner : MonoBehaviour
{
    //Cardを実際にプレイする箇所
    [SerializeField] private StateDealer state;
    [SerializeField] private string skillingState;
    [SerializeField] private CardDealer dealer;

    public void CardPlay(List<CardSkill> skills)
    {
        Debug.Log("Runner");
        dealer.SkillStacking(skills);
        dealer.StackRun();
        //state.ChangeState(skillingState);
    }
}