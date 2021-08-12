using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugObject : MonoBehaviour, ICursolable
{
    public void Click(Vector3 point, ContactMode mode)
    {
        if (mode == ContactMode.Stay)
        {
            Vector3 drugPos = Camera.main.ScreenToWorldPoint(point);
            transform.position = new Vector3(drugPos.x, drugPos.y, transform.position.z);
        }

    }
    public void Cursol(Vector3 point)
    {

    }
}
