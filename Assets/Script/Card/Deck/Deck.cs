using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

#pragma warning disable 0649
[System.Serializable]
public class Deck
{
    //カードを纏める所
    public List<Card> initCards;
    private ReactiveCollection<IDealableCard> _cards = new ReactiveCollection<IDealableCard>(new List<IDealableCard>());

    //中身の値だけを公開するためのList(このListの値を変えてもReactiveCollection側は変わらない)
    public List<IDealableCard> cards => _cards.ToList();
    //ReactiveCollectionのうちIObservableだけを公開し、処理を登録できるように
    public IObservable<CollectionReplaceEvent<IDealableCard>> ObservableReplace => _cards.ObserveReplace();
    public IObservable<CollectionAddEvent<IDealableCard>> ObservableAdd => _cards.ObserveAdd();
    public IObservable<CollectionRemoveEvent<IDealableCard>> ObservableRemove => _cards.ObserveRemove();
    public void InspectorInit()
    {
        Substitution(initCards.Select(x => { return new DefaultDealableCard(x, null) as IDealableCard; }).ToList());
    }
    public void Substitution(List<IDealableCard> c)
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

    public void SubstitutionCard(List<Card> c)
    {
        foreach (var i in c.Select((Value, Index) => new { Value, Index }))
        {
            if (i.Index < _cards.Count)
            {
                _cards[i.Index].SetCard(i.Value);
            }
            else _cards.Add(new DefaultDealableCard(i.Value, null) as IDealableCard);
        }

        if (_cards.Count > c.Count)
        {
            for (int i = _cards.Count - 1; i > c.Count - 1; i--)
            {
                _cards.RemoveAt(i);
            }
        }

    }

    public void Add(IDealableCard c)
    {
        _cards.Add(c);
    }
    public void Add(List<IDealableCard> cs)
    {
        foreach (IDealableCard c in cs)
        {
            _cards.Add(c);
        }
    }

    public void Remove(IDealableCard c) { _cards.Remove(c); }
    public void Remove(List<IDealableCard> cs)
    {
        foreach (IDealableCard c in cs)
        {
            _cards.Remove(c);
        };
    }

    public List<IDealableCard> Draw(int n)
    {
        if (1 <= n)
        {
            if (n <= _cards.Count)
            {
                List<IDealableCard> returnCards = _cards.ToList().GetRange(0, n);
                for (int i = 0; i < n; i++)
                {
                    _cards.RemoveAt(0);
                }
                return returnCards;
            }
            else
            {
                List<IDealableCard> returnCards = _cards.ToList();
                _cards.Clear();
                return returnCards;

            }
        }
        return null;
    }

    public List<IDealableCard> DrawCheck(int i)
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
    public static List<IDealableCard> shuffle(List<IDealableCard> c)
    {
        return c.OrderBy(a => Guid.NewGuid()).ToList();
    }
}
