using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

[System.Serializable]
public class DefaultDeck : IStagingDeck, IObservableDeck
{
    //カードを纏める所
    public DeckType deckType { get; private set; }
    public List<CardData> initCards = new List<CardData>();
    [SerializeReference, SubclassSelector] public IPermanentFactory factory;
    private ReactiveCollection<IPermanent> _permanents = new ReactiveCollection<IPermanent>(new List<IPermanent>());

    //中身の値だけを公開するためのList(このListの値を変えてもReactiveCollection側は変わらない)
    public List<ICard> cards => _permanents.Select(x => { return x.GetCard(); }).ToList();
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
            if (i.Index < _permanents.Count)
            {
                _permanents[i.Index] = factory.CardMake(i.Value, this);
            }
            else Add(i.Value);
        }

        if (_permanents.Count > c.Count)
        {
            for (int i = _permanents.Count - 1; i > c.Count - 1; i--)
            {
                IPermanent removePer = _permanents[i];
                _permanents.RemoveAt(i);
                removePer.Dispose();
            }
        }

    }

    public bool Add(ICard c)
    {
        _permanents.Add(factory.CardMake(c, this));
        return true;
    }

    public bool AddCheck(ICard c)
    {
        return true;
    }

    public bool Remove(ICard c)
    {
        if (!_permanents.Any(x => { return x.GetCard() == c; })) return false;

        IPermanent removePer = _permanents.Where(x => { return x.GetCard() == c; }).First();
        _permanents.Remove(removePer);
        removePer.Dispose();
        return true;
    }
    public bool RemoveCheck(ICard c)
    {
        if (!_permanents.Any(x => { return x.GetCard() == c; })) return false;
        return true;
    }
    public bool ExistCheck(ICard c)
    {
        return _permanents.Select(x => { return x.GetCard(); }).Contains(c);
    }
    public int Count()
    {
        return _permanents.Count;
    }
    public bool Any()
    {
        return _permanents.Any();
    }

    public List<ICard> Draw(int n)
    {
        if (1 <= n)
        {
            if (n <= _permanents.Count)
            {
                List<ICard> returnCards = _permanents.Select(x => { return x.GetCard(); }).ToList().GetRange(0, n);
                for (int i = 0; i < n; i++)
                {
                    _permanents.RemoveAt(0);
                }
                return returnCards;
            }
            else
            {
                List<ICard> returnCards = _permanents.Select(x => { return x.GetCard(); }).ToList();
                _permanents.Clear();
                return returnCards;

            }
        }
        return null;
    }

    public List<ICard> DrawCheck(int i)
    {
        if (1 <= i)
        {
            if (i <= _permanents.Count)
            {
                return _permanents.Select(x => { return x.GetCard(); }).ToList().GetRange(0, i - 1);
            }
            else
            {
                return _permanents.Select(x => { return x.GetCard(); }).ToList();
            }
        }
        return null;
    }
    public void Shuffle()
    {
        _permanents.OrderBy(a => Guid.NewGuid());
    }
    //Enumerableの実装
    public IEnumerator<IPermanent> GetEnumerator()
    {
        return _permanents.ToList().GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _permanents.ToList().GetEnumerator();
    }
    //ReactiveCollectionのうちIObservableだけを公開し、処理を登録できるように
    public IObservable<CollectionReplaceEvent<IPermanent>> ReplaceEvent()
    {
        return _permanents.ObserveReplace();
    }
    public IObservable<CollectionAddEvent<IPermanent>> AddEvent()
    {
        return _permanents.ObserveAdd();
    }
    public IObservable<CollectionRemoveEvent<IPermanent>> RemoveEvent()
    {
        return _permanents.ObserveRemove();
    }
}
