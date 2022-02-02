using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

public class Deck : IStagingDeck
{
    //カードを纏める所
    public DeckType deckType { get; private set; }
    public List<CardData> initCards = new List<CardData>();
    private ReactiveCollection<ICard> _cards = new ReactiveCollection<ICard>(new List<ICard>());

    //中身の値だけを公開するためのList(このListの値を変えてもReactiveCollection側は変わらない)
    public List<ICard> cards => _cards.ToList();

    public void Init(DeckType type)
    {
        deckType = type;
        Substitution(initCards.Select(x => { return new DefaultCard(x, this) as ICard; }).ToList());
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
                _cards[i.Index] = new DefaultCard(i.Value, this) as ICard;
            }
            else _cards.Add(new DefaultCard(i.Value, this) as ICard);
        }

        if (_cards.Count > c.Count)
        {
            for (int i = _cards.Count - 1; i > c.Count - 1; i--)
            {
                _cards.RemoveAt(i);
            }
        }

    }

    public bool Add(ICard c)
    {
        _cards.Add(c);
        return true;
    }

    public bool Remove(ICard c)
    {
        return _cards.Remove(c);
    }
    public bool ExistCheck(ICard c)
    {
        return _cards.Contains(c);
    }
    public int Count()
    {
        return _cards.Count;
    }
    public bool Any()
    {
        return _cards.Any();
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
    //ReactiveCollectionのうちIObservableだけを公開し、処理を登録できるように
    public IObservable<CollectionReplaceEvent<ICard>> ReplaceEvent()
    {
        return _cards.ObserveReplace();
    }
    public IObservable<CollectionAddEvent<ICard>> AddEvent()
    {
        return _cards.ObserveAdd();
    }
    public IObservable<CollectionRemoveEvent<ICard>> RemoveEvent()
    {
        return _cards.ObserveRemove();
    }
    public List<ICard> Cards()
    {
        return cards;
    }
}
