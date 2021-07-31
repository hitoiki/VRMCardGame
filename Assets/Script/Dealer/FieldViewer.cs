using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
#pragma warning disable 0649
public class FieldViewer : MonoBehaviour, IGameState
{
    //CardFieldを表示する
    //CardPrefabを用いてインスタンス化する
    //UI辺りの兼ね合いはよく分からん
    [SerializeField] private CardField cardField;
    [SerializeField] private List<GameObject> InitField = new List<GameObject>(1);
    private List<ICardPrinted> fieldPrinted;

    private IDisposable _fieldPrint;


    private void OnValidate()
    {
        if (InitField != null) InitField = InitField.Where(x => { return (x == null) || (x.GetComponent<ICardPrinted>() != null); }).ToList();
    }

    private void Start()
    {
        fieldPrinted = InitField.Select(x => { return x.GetComponent<ICardPrinted>(); }).ToList();
    }

    //Start
    public void CrankIn()
    {
        cardField.field.ObservableCards.Subscribe(x =>
        {
            DeckCheck(cardField.field.cards);

        });
        DeckCheck(cardField.field.cards);
    }
    //Update
    public void StateUpdate()
    {

    }
    //End
    public void CrankUp()
    {
        Debug.Log("CrankUp");
        //購読停止
        _fieldPrint.Dispose();
    }

    private void DeckCheck(List<Card> c)
    {
        if (c != null)
        {
            Debug.Log("FieldViewer:Updated");
            foreach (var i in c.Select((Card card, int index) => new { card, index }))
            {
                if (i.index < fieldPrinted.Count()) fieldPrinted[i.index].Print(i.card);
                else break;
            }
        }
    }

}