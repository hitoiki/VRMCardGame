using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

[System.Serializable]
public class Deck : MonoBehaviour
{
#pragma warning disable 0649
    //カードを纏める所
    public List<CardData> initCards;
    private ReactiveCollection<Card> _cards = new ReactiveCollection<Card>(new List<Card>());

    //中身の値だけを公開するためのList(このListの値を変えてもReactiveCollection側は変わらない)
    public List<Card> cards => _cards.ToList();
    //ReactiveCollectionのうちIObservableだけを公開し、処理を登録できるように
    public IObservable<CollectionReplaceEvent<Card>> ObservableReplace => _cards.ObserveReplace();
    public IObservable<CollectionAddEvent<Card>> ObservableAdd => _cards.ObserveAdd();
    public IObservable<CollectionRemoveEvent<Card>> ObservableRemove => _cards.ObserveRemove();
    private void Start()
    {
        Substitution(initCards.Select(x => { return new Card(x); }).ToList());
    }
    public void Substitution(List<Card> c)
    {
        //代入する奴です
        foreach (var i in c.Select((Value, Index) => new { Value, Index }))
        {
            if (i.Index < _cards.Count)
            {
                _cards[i.Index] = i.Value;
            }
            else _cards.Add(i.Value);
        }

        if (_cards.Count > c.Count)
        {
            for (int i = _cards.Count - 1; i > c.Count - 1; i--)
            {
                _cards.RemoveAt(i);
            }
        }

    }

    public void Add(Card c)
    {
        _cards.Add(c);
    }
    public void Add(List<Card> cs)
    {
        foreach (Card c in cs)
        {
            _cards.Add(c);
        }
    }

    public void Remove(Card c) { _cards.Remove(c); }
    public void Remove(List<Card> cs)
    {
        foreach (Card c in cs)
        {
            _cards.Remove(c);
        };
    }

    public List<Card> Draw(int n)
    {
        if (1 <= n)
        {
            if (n <= _cards.Count)
            {
                List<Card> returnCards = _cards.ToList().GetRange(0, n);
                for (int i = 0; i < n; i++)
                {
                    _cards.RemoveAt(i);
                }
                return returnCards;
            }
            else
            {
                List<Card> returnCards = _cards.ToList();
                _cards.Clear();
                return returnCards;

            }
        }
        return null;
    }

    public List<Card> DrawCheck(int i)
    {
        if (1 <= i)
        {
            if (i <= _cards.Count)
            {
                return _cards.ToList().GetRange(0, i - 1);
            }
            else
            {
                return _cards.ToList();
            }
        }
        return null;
    }

    public Card OneDraw()
    {
        Card returncard = Draw(1).First();
        return returncard;
    }
    public static List<Card> shuffle(List<Card> c)
    {
        return c.OrderBy(a => Guid.NewGuid()).ToList();
    }
}
