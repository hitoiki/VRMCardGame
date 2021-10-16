using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayRunner : MonoBehaviour
{
    //Cardを実際にプレイする箇所
    [SerializeField] private StateDealer state;
    [SerializeField] private string skillingState;
    [SerializeField] private CardDealer dealer;

    public void CardPlay(CardSkill skill)
    {
        skill.skill(dealer);
        state.ChangeState(skillingState);
    }
}