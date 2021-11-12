using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CursolTeleportObject : ICardCursolEvent
{
    [SerializeField] private GameObject[] portObj;
    [SerializeField] float zPos;
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
        if (mode == ContactMode.Enter)
        {
            foreach (GameObject obj in portObj)
            {
                obj.SetActive(true);
            }
        }
        foreach (GameObject obj in portObj)
        {
            obj.transform.position = new Vector3(
                card.GetDealableCard().GetTransform().position.x,
                card.GetDealableCard().GetTransform().position.y,
                zPos);
        }

    }
}