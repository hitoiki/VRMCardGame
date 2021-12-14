using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

#pragma warning disable 0649
[System.Serializable]
public class Deck : IDeck
{
    //カードを纏める所
    public DeckType deckType { get; private set; }
    public List<CardData> initCards;
    private ReactiveCollection<ICard> _cards = new ReactiveCollection<ICard>(new List<ICard>());

    //中身の値だけを公開するためのList(このListの値を変えてもReactiveCollection側は変わらない)
    public List<ICard> cards => _cards.ToList();
    //ReactiveCollectionのうちIObservableだけを公開し、処理を登録できるように
    public IObservable<CollectionReplaceEvent<ICard>> ObservableReplace => _cards.ObserveReplace();
    public IObservable<CollectionAddEvent<ICard>> ObservableAdd => _cards.ObserveAdd();
    public IObservable<CollectionRemoveEvent<ICard>> ObservableRemove => _cards.ObserveRemove();
    public void InspectorInit(DeckType type)
    {
        deckType = type;
        Substitution(initCards.Select(x => { return new DefaultCard(x) as ICard; }).ToList());
    }

    public DeckType GetDeckType()
    {
        return deckType;
    }
    public void Substitution(List<ICard> c)
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

    public void SubstitutionCard(List<CardData> c)
    {
        foreach (var i in c.Select((Value, Index) => new { Value, Index }))
        {
            if (i.Index < _cards.Count)
            {
                _cards[i.Index] = new DefaultCard(i.Value) as ICard;
            }
            else _cards.Add(new DefaultCard(i.Value) as ICard);
        }

        if (_cards.Count > c.Count)
        {
            for (int i = _cards.Count - 1; i > c.Count - 1; i--)
            {
                _cards.RemoveAt(i);
            }
        }

    }

    public void Add(ICard c)
    {
        _cards.Add(c);
    }
    public void Add(List<ICard> cs)
    {
        foreach (ICard c in cs)
        {
            _cards.Add(c);
        }
    }

    public void Remove(ICard c) { _cards.Remove(c); }
    public void Remove(List<ICard> cs)
    {
        foreach (ICard c in cs)
        {
            _cards.Remove(c);
        };
    }

    public List<ICard> Pick(List<ICard> cs)
    {
        List<ICard> returnCards = new List<ICard>();
        foreach (ICard c in cs)
        {
            if (_cards.Contains(c)) continue;
            returnCards.Add(c);
            _cards.Remove(c);
        }
        return returnCards;
    }

    public bool ExistCheck(ICard c)
    {
        return _cards.Contains(c);
    }

    public List<ICard> Draw(int n)
    {
        if (1 <= n)
        {
            if (n <= _cards.Count)
            {
                List<ICard> returnCards = _cards.ToList().GetRange(0, n);
                for (int i = 0; i < n; i++)
                {
                    _cards.RemoveAt(0);
                }
                return returnCards;
            }
            else
            {
                List<ICard> returnCards = _cards.ToList();
                _cards.Clear();
                return returnCards;

            }
        }
        return null;
    }

    public List<ICard> DrawCheck(int i)
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
    public static List<ICard> CardListShuffle(List<ICard> c)
    {
        return c.OrderBy(a => Guid.NewGuid()).ToList();
    }

    public void Shuffle()
    {
        _cards.OrderBy(a => Guid.NewGuid());
    }
}
