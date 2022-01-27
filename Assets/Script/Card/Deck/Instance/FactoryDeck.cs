using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FactoryDeck : IDeck
{
    //ICardFactoryと連動しているデッキ
    //主にeffectの処理の為に使われる
    [SerializeReference, SubclassSelector] IDeck deck;
    public DeckType GetDeckType()
    {
        return deck.GetDeckType();
    }
    public void Substitution(List<ICard> c)
    {
        deck.Substitution(c);
    }


    public bool Add(ICard c)
    {
        return deck.Add(c);
    }

    public bool Remove(ICard c)
    {
        return deck.Remove(c);
    }
    public bool ExistCheck(ICard c)
    {
        return deck.ExistCheck(c);
    }
    public int Count()
    {
        return deck.Count();
    }
    public bool Any()
    {
        return deck.Any();
    }

    public List<ICard> Draw(int n)
    {
        return deck.Draw(n);
    }

    public List<ICard> DrawCheck(int i)
    {
        return deck.DrawCheck(i);
    }

    public void Shuffle()
    {
        deck.Shuffle();
    }
}
