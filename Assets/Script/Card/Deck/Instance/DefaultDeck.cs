using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

[System.Serializable]
public class DefaultDeck : IStagingDeck
{
    //カードを纏める所
    public DeckType deckType { get; private set; }
    public List<CardData> initCards = new List<CardData>();
    [SerializeReference, SubclassSelector] public IPermanentFactory factory;
    private ReactiveCollection<ICard> _cards = new ReactiveCollection<ICard>(new List<ICard>());

    //中身の値だけを公開するためのList(このListの値を変えてもReactiveCollection側は変わらない)
    public List<ICard> cards => _cards.ToList();
    public void Init(DeckType type)
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
    public void Shuffle()
    {
        _cards.OrderBy(a => Guid.NewGuid());
    }
    //Enumerableの実装
    public IEnumerator<IPermanent> GetEnumerator()
    {
        return _cards.Select(x => { return factory.CardMake(x, this); }).ToList().GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _cards.Select(x => { return factory.CardMake(x, this); }).ToList().GetEnumerator();
    }
    //ReactiveCollectionのうちIObservableだけを公開し、処理を登録できるように
    public IObservable<CollectionReplaceEvent<IPermanent>> ReplaceEvent()
    {
        return _cards.ObserveReplace().Select(x =>
        {
            return new CollectionReplaceEvent<IPermanent>(x.Index, factory.CardMake(x.OldValue, this), factory.CardMake(x.NewValue, this));
        });
    }
    public IObservable<CollectionAddEvent<IPermanent>> AddEvent()
    {
        return _cards.ObserveAdd().Select(x =>
        {
            return new CollectionAddEvent<IPermanent>(x.Index, factory.CardMake(x.Value, this));
        });
    }
    public IObservable<CollectionRemoveEvent<IPermanent>> RemoveEvent()
    {
        return _cards.ObserveRemove().Select(x =>
        {
            return new CollectionRemoveEvent<IPermanent>(x.Index, factory.CardMake(x.Value, this));
        });
    }
}
