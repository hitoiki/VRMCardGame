using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLinkedObject : MonoBehaviour, IGameState
{
    public List<GameObject> objects;
    public void CrankIn()
    {
        if (objects == null) return;
        foreach (GameObject o in objects)
        {
            o.SetActive(true);
        }
    }

    public void StateUpdate()
    {

    }

    public void CrankUp()
    {
        if (objects == null) return;
        foreach (GameObject o in objects)
        {
            o.SetActive(false);
        }
    }
}