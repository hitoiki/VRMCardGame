using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleState : MonoBehaviour, IGameState
{
    public string logWord;
    public void CrankIn()
    {
        Debug.Log(logWord + "CrankIn");
    }
    //Update
    public void StateUpdate()
    {
        Debug.Log(logWord + "Update");
    }
    //End
    public void CrankUp()
    {
        Debug.Log(logWord + "CrankUp");
    }

}
