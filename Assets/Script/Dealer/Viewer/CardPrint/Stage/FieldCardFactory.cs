using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCardFactory : MonoBehaviour, ICardFactory
{
    //FieldCardを作って渡すやつ
    [SerializeField] private FieldCard fieldCard;
    [SerializeField] private GameObject initVRM = null;
    [SerializeField] private GameObject initCoinSprite = null;
    private ICardPrintable vrmPrintable = null;
    private ObjectFlyer<FieldCard> fieldCardFlyer = null;
    private ObjectFlyer<CoinSprite> coinSpriteFlyer = null;

    private List<ICardPrintable> printableList = new List<ICardPrintable>();

    private void OnValidate()
    {
        if (initVRM == null || initVRM.GetComponent<ICardPrintable>() == null) initVRM = null;
        if (initCoinSprite == null || initCoinSprite?.GetComponent<CoinSprite>() == null) initCoinSprite = null;
    }

    private void Start()
    {
        //InitHandがICardPrintedである事が前提条件なアレ
        vrmPrintable = initVRM?.GetComponent<ICardPrintable>();
        fieldCardFlyer = new ObjectFlyer<FieldCard>(fieldCard);
        coinSpriteFlyer = new ObjectFlyer<CoinSprite>(initCoinSprite.GetComponent<CoinSprite>());

    }

    public ICardPrintable CardMake(Card card, Vector3 position)
    {
        FieldCard f = fieldCardFlyer.GetMob(position);
        f.vrmPrinted = vrmPrintable;
        f.coinCard.flyer = coinSpriteFlyer;
        printableList.Add(f);
        return (ICardPrintable)f;
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
