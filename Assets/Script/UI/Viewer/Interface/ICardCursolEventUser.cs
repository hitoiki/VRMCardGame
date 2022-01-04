using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardCursolEventUser
{
    void AddCardCursolEvent(ICardCursolEvent cursolEvent);
    void RemoveCardCursolEvent(ICardCursolEvent cursolEvent);
    void SubstitutionCardCursolEvent(List<ICardCursolEvent> cursolEvent);
}
