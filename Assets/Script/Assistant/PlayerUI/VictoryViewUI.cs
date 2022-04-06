using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VictoryViewUI : MonoBehaviour, IGameState
{
    [SerializeField] PlayerData player;
    [SerializeField] Text text;

    public void CrankIn()
    {
        text.text = "スコア：" + player.winPoint.ToString();
    }
    //Update
    public void StateUpdate()
    {

    }
    //End
    public void CrankUp()
    {

    }
}
