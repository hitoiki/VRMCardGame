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
    public List<Card> initCards;
    private ReactiveCollection<Card> _cards = new ReactiveCollection<Card>(new List<Card>());

    //中身の値だけを公開するためのList(このListの値を変えてもReactiveCollection側は変わらない)
    public List<Card> cards => _cards.ToList();
    //ReactiveCollectionのうちIObservableだけを公開し、処理を登録できるように
    public IObservable<CollectionReplaceEvent<Card>> ObservableCards => _cards.ObserveReplace();
    private void Start()
    {
        Substitution(initCards);
    }


    //テスト用 時期を見て消す
    private void Update()
    {
        Substitution(initCards);
    }

    public void Substitution(List<Card> c)
    {
        //代入する奴です
        foreach (var i in c.Select((Value, Index) => new { Value, Index }))
        {
            if (i.Index < _cards.Count)
            {
                if (_cards[i.Index] != i.Value) _cards[i.Index] = i.Value;
            }
            else _cards.Add(i.Value);
        }

        if (_cards.Count > c.Count)
        {
            for (int i = c.Count; i < _cards.Count; i++)
            {
                _cards.RemoveAt(i);
            }
        }

    }

    public List<Card> Draw(int i)
    {
        if (0 <= i)
        {
            if (i <= _cards.Count)
            {
                List<Card> returnCards = _cards.ToList().GetRange(0, i - 1);
                for (int x = i; x <= _cards.Count(); x++)
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
