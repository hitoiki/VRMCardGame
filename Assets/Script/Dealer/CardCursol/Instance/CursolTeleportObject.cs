using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CursolTeleportObject : ICardCursolEvent
{
    [SerializeField] private GameObject[] portObj;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Exit)
        {
            foreach (GameObject obj in portObj)
            {
                obj.SetActive(false);
            }
            return;
        }
        foreach (GameObject obj in portObj)
        {
            obj.transform.position = new Vector3(
                card.GetTransform().position.x,
                card.GetTransform().position.y,
                obj.transform.position.z);
        }
        if (mode == ContactMode.Enter)
        {
            foreach (GameObject obj in portObj)
            {
                obj.SetActive(true);
            }
        }

    }
}