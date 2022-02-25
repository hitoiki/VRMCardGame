using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCursolClickSkill : ICardCursolEvent
{
    [SerializeField] CardPlayDealer dealer;
    public void CardClick(ICardPrintable card, Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Enter)
        {
            dealer.CardPlay(card.GetPermanent().GetSkillPack().SkillProcess<OtherSkillKind>(OtherSkillKind.Click), card.GetPermanent());
        }
    }
    public void CardCursol(ICardPrintable card, Vector3 pos, ContactMode mode)
    {

    }
    public void Open(ICardPrintable card)
    {

    }
    public void Close(ICardPrintable card)
    {
    }

}
