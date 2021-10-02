using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class HandCardFactory : MonoBehaviour, ICardFactory
{
    [SerializeField] private GameObject initVRM = null;
    [SerializeField] private HandCard handCard = null;
    [SerializeField] private CardPlayRecepter recepter = null;
    private ICardPrintable vrmPrinted = null;
    private ObjectFlyer<HandCard> flyer;

    private List<ICardPrintable> printableList = new List<ICardPrintable>();

    private void OnValidate()
    {
        if ((initVRM != null) && initVRM.GetComponent<ICardPrintable>() == null) initVRM = null;

    }

    private void Start()
    {
        //InitHandがICardPrintedである事が前提条件なアレ
        if (initVRM != null) vrmPrinted = initVRM.GetComponent<ICardPrintable>();
        if (handCard != null) flyer = new ObjectFlyer<HandCard>(handCard);
    }
    public ICardPrintable CardMake(Card card, Vector3 position)
    {
        HandCard printedObj = flyer.GetMob(position, y =>
        {
            y.vrmPrinted = vrmPrinted;
            y.anchor = position;
            y.recepter = recepter;
        }
        , y => { y.Active(true); });
        printableList.Add(printedObj);
        return printedObj;
    }

    public void CardErace(ICardPrintable printable)
    {
        printable.UnPrint();
        printable.Active(false);
        printableList.Remove(printable);
    }

    public List<ICardPrintable> GetCards()
    {
        return printableList;
    }
}
