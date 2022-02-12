using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class SkillDealingDeckDeco : IStagingDeck
{
    //カードを纏める所
    [SerializeReference, SubclassSelector] public IStagingDeck deck;
    private SkillQueue skillQueue;
    private List<IDisposable> subjectList;
    public void Init(DeckType type, SkillQueue queue)
    {
        deck.Init(type, queue);
        skillQueue = queue;
    }

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
        //Kouji
        Observable.Merge<(Coin, int)>(
           c.GetObserveCoin().ObserveAdd().Select(x => { return (x.Key, x.Value); }),
           c.GetObserveCoin().ObserveReplace().Select(x => { return (x.Key, x.NewValue); })
           );
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
    //Enumerableの実装
    public IEnumerator<ICard> GetEnumerator()
    {
        return deck.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return deck.GetEnumerator();
    }
    //ReactiveCollectionのうちIObservableだけを公開し、処理を登録できるように
    public IObservable<CollectionReplaceEvent<ICard>> ReplaceEvent()
    {
        return deck.ReplaceEvent();
    }
    public IObservable<CollectionAddEvent<ICard>> AddEvent()
    {
        return deck.AddEvent();
    }
    public IObservable<CollectionRemoveEvent<ICard>> RemoveEvent()
    {
        return deck.RemoveEvent();
    }
}
