using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectCursolEvent : MonoBehaviour, ICardCursolEvent
{
    //これが場のカード分呼び出されている可能性がちょっとある
    [SerializeField] CardPlayRunner runner;
    public Card selectingCard;
    public ISelectSkill selectSkill;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Enter)
        {
            List<Card> selectedCards = new List<Card>();
            selectedCards.Add(card.GetCard());
            Debug.Log(card.GetTransform().gameObject.name);
            runner.CardPlay(selectSkill.SelectSkill(selectingCard, selectedCards));

        }
    }
    public void CardCursol(ICardPrintable card, Vector3 pos)
    {

    }
}
