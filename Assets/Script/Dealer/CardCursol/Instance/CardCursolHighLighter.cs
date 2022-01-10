using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCursolHighLighter : ICardCursolEvent
{
    [SerializeField] StageCardViewer viewer;
    [SerializeField] private GameObject initFactory;
    private ICardFactory factory;
    private bool Clicking;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (factory == null) factory = initFactory.GetComponent<ICardFactory>();
        if (factory == null) return;
        Clicking = !(mode == ContactMode.Exit);
        if (mode == ContactMode.Exit)
        {
            viewer.grid.HighLightIndex(-1);
            viewer.AllAlign();
        }
    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (factory == null) factory = initFactory.GetComponent<ICardFactory>();
        if (factory == null) return;
        if (Clicking) return;
        if (mode == ContactMode.Stay) return;
        if (mode == ContactMode.Enter) viewer.grid.HighLightIndex(factory.GetCards().IndexOf(card));
        if (mode == ContactMode.Exit) viewer.grid.HighLightIndex(-1);
        viewer.AllAlign();
    }
    public void Close(ICardPrintable card, Vector3 pos)
    {
        viewer.grid.HighLightIndex(-1);
        viewer.AllAlign();
    }
}