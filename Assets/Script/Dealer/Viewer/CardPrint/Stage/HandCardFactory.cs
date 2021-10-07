using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class HandCardFactory : MonoBehaviour, ICardFactory
{
    [SerializeField] private List<GameObject> initCursol = new List<GameObject>();
    [SerializeField] private HandCard handCard = null;
    private List<ICursolableCard> firstCursols = new List<ICursolableCard>();
    private ObjectFlyer<HandCard> flyer;

    private List<HandCard> printableList = new List<HandCard>();

    private void OnValidate()
    {
        if (initCursol != null) initCursol = initCursol.Where(x => { return (x == null) || (x.GetComponent<ICursolableCard>() != null); }).ToList();

    }

    private void Start()
    {
        //InitHandがICardPrintedである事が前提条件なアレ
        firstCursols = initCursol.SelectMany(x => { return x.GetComponents<ICursolableCard>(); }).ToList();
        if (handCard != null) flyer = new ObjectFlyer<HandCard>(handCard);
    }
    public ICardPrintable CardMake(Card card, Vector3 position)
    {
        HandCard printedObj = flyer.GetMob(position, y =>
        {
            y.cursolable.AddRange(firstCursols);
            y.anchor = position;
        }
        , y => { y.Active(true); });
        printableList.Add(printedObj);
        return printedObj;
    }

    public void CardEraceAt(int index)
    {
        printableList[index].UnPrint();
        printableList[index].Active(false);
        printableList.RemoveAt(index);
    }

    public List<ICardPrintable> GetCards()
    {
        return printableList.Select(x => { return x as ICardPrintable; }).ToList(); ;
    }
}
