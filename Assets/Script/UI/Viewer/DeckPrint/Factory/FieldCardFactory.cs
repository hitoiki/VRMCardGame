using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FieldCardFactory : MonoBehaviour, ICardFactory, ICardCursolEventUser
{
    //FieldCardを作って渡すやつ
    [SerializeField] private Transform bundle;
    [SerializeField] private FieldCard fieldCard;
    [SerializeField] private List<GameObject> initCursol = new List<GameObject>();
    [SerializeField] private GameObject initCoinSprite = null;
    private List<ICardCursolEvent> firstCursols = new List<ICardCursolEvent>();
    public ObjectFlyer<FieldCard> fieldCardFlyer = null;
    private ObjectFlyer<CoinSprite> coinSpriteFlyer = null;

    public List<FieldCard> printableList = new List<FieldCard>();

    private void OnValidate()
    {
        if (initCursol != null) initCursol = initCursol.Where(x => { return (x == null) || (x.GetComponent<ICardCursolEvent>() != null); }).ToList();

        if (initCoinSprite == null || initCoinSprite?.GetComponent<CoinSprite>() == null) initCoinSprite = null;
    }

    private void Start()
    {
        //InitHandがICardPrintedである事が前提条件なアレ
        firstCursols = initCursol.SelectMany(x => { return x.GetComponents<ICardCursolEvent>(); }).ToList();
        fieldCardFlyer = new ObjectFlyer<FieldCard>(fieldCard);
        coinSpriteFlyer = new ObjectFlyer<CoinSprite>(initCoinSprite.GetComponent<CoinSprite>());
    }

    public ICardPrintable CardMake(ICard card, Vector3 position)
    {
        FieldCard f = fieldCardFlyer.GetMob(position, y =>
        {
            if (bundle != null) y.transform.SetParent(bundle);
            y.cursolEvent.AddRange(firstCursols);
            y.coinCard.flyer = coinSpriteFlyer;
        });
        printableList.Add(f);
        return (ICardPrintable)f;
    }

    public void CardEraceAt(int index)
    {
        printableList[index].UnPrint();
        printableList[index].Active(false);
        printableList.RemoveAt(index);
    }

    public List<ICardPrintable> GetCards()
    {
        return printableList.Select(x => { return x as ICardPrintable; }).ToList();
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
