using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableBox : MonoBehaviour, ICursolableCard
{
    [SerializeField] CardPlayChecker checker;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
    public void CardCursol(ICardPrintable card, Vector3 pos)
    {

    }
}
