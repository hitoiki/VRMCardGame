using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupervisedSetter : MonoBehaviour
{
    public List<SupervisedObject> follower = null;
    public Superviser viser;
    void Start()
    {
        foreach (SupervisedObject f in follower)
        {
            viser.follower.Add(f);
        }
    }
}
