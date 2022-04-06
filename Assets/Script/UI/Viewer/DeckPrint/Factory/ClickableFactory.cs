using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClickableFactory : MonoBehaviour, ICardPrintableFactory, ICardCursolEventUser
{
    [SerializeField] private List<GameObject> initCursol = new List<GameObject>();
    [SerializeField] private ClickableCard clickableCard = null;
    private List<ICardCursolEvent> firstCursols = new List<ICardCursolEvent>();
    private ObjectFlyer<ClickableCard> flyer;

    private List<ClickableCard> printableList = new List<ClickableCard>();

    private void OnValidate()
    {
        if (initCursol != null) initCursol = initCursol.Where(x => { return (x == null) || (x.GetComponent<ICardCursolEvent>() != null); }).ToList();

    }

    private void Start()
    {
        //InitHandがICardPrintedである事が前提条件なアレ
        firstCursols = initCursol.SelectMany(x => { return x.GetComponents<ICardCursolEvent>(); }).ToList();
        if (clickableCard != null) flyer = new ObjectFlyer<ClickableCard>(clickableCard);
    }
    public ICardPrintable CardMake(IPermanent card, Vector3 position)
    {
        ClickableCard printedObj = flyer.GetMob(position, y =>
        {
            y.Init();
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
        foreach (ICardCursolEventUser u in flyer.MobList.Select(x => { return x as ICardCursolEventUser; }))
        {
            u.AddCardCursolEvent(cursolEvent);
        }
    }
    public void RemoveCardCursolEvent(ICardCursolEvent cursolEvent)
    {
        firstCursols.Remove(cursolEvent);
        foreach (ICardCursolEventUser u in flyer.MobList.Select(x => { return x as ICardCursolEventUser; }))
        {
            u.RemoveCardCursolEvent(cursolEvent);
        }
    }
    public void SubstitutionCardCursolEvent(List<ICardCursolEvent> cursolEvent)
    {
        firstCursols = cursolEvent;
        foreach (ICardCursolEventUser u in flyer.MobList.Select(x => { return x as ICardCursolEventUser; }))
        {
            u.SubstitutionCardCursolEvent(cursolEvent);
        }
    }
}
