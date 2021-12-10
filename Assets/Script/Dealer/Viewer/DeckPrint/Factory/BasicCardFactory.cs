using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class BasicCardFactory : MonoBehaviour, ICardFactory, ICardCursolEventUser
{
    [SerializeField] private Transform bundle;
    [SerializeField] private List<GameObject> initCursol = new List<GameObject>();
    [SerializeField] private BasicCard basicCard = null;
    private List<ICardCursolEvent> firstCursols = new List<ICardCursolEvent>();
    private ObjectFlyer<BasicCard> flyer;

    private List<BasicCard> printableList = new List<BasicCard>();

    private void OnValidate()
    {
        if (initCursol != null) initCursol = initCursol.Where(x => { return (x == null) || (x.GetComponent<ICardCursolEvent>() != null); }).ToList();

    }

    private void Start()
    {
        //InitHandがICardPrintedである事が前提条件なアレ
        firstCursols = initCursol.SelectMany(x => { return x.GetComponents<ICardCursolEvent>(); }).ToList();
        if (basicCard != null) flyer = new ObjectFlyer<BasicCard>(basicCard);
    }
    public ICardPrintable CardMake(ICard card, Vector3 position)
    {
        BasicCard printedObj = flyer.GetMob(position, y =>
        {
            if (bundle != null) y.transform.SetParent(bundle);
            y.cursolEvent.AddRange(firstCursols);
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
    public void AddCardCursolEvent(ICardCursolEvent cursolEvent)
    {
        firstCursols.Add(cursolEvent);
        foreach (ICardCursolEventUser u in printableList.Select(x => { return x as ICardCursolEventUser; }))
        {
            u.AddCardCursolEvent(cursolEvent);
        }
    }
    public void RemoveCardCursolEvent(ICardCursolEvent cursolEvent)
    {
        firstCursols.Remove(cursolEvent);
        foreach (ICardCursolEventUser u in printableList.Select(x => { return x as ICardCursolEventUser; }))
        {
            u.RemoveCardCursolEvent(cursolEvent);
        }
    }
    public void SubstitutionCardCursolEvent(List<ICardCursolEvent> cursolEvent)
    {
        firstCursols = cursolEvent;
        foreach (ICardCursolEventUser u in printableList.Select(x => { return x as ICardCursolEventUser; }))
        {
            u.SubstitutionCardCursolEvent(cursolEvent);
        }
    }
}