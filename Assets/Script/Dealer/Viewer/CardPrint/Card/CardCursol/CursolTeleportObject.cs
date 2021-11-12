using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursolTeleportObject : MonoBehaviour, ICardCursolEvent
{
    [SerializeField] private GameObject portObj;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Exit)
        {
            portObj.SetActive(false);
            return;
        }
        if (mode == ContactMode.Enter)
        {
            portObj.SetActive(true);
        }
        portObj.transform.position = new Vector3(
            card.GetDealableCard().GetTransform().position.x,
            card.GetDealableCard().GetTransform().position.y,
            transform.position.z);

    }
}