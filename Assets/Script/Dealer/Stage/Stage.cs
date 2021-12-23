using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Stage : MonoBehaviour
{
    //Cardを置く盤面、舞台
    //コイツを読んで処理したりしなかったりする
    //データ等は主にここで握る
    [SerializeField] public Deck hands;
    [SerializeField] public Deck field;
    [SerializeField] public Deck disCard;
    [SerializeField] public Deck trace;
    [SerializeField] public Deck senter;
    [SerializeField] public Deck right;
    [SerializeField] public Deck left;

    public SkillQueueObject queueObject = new SkillQueueObject();

    public HighOrderRule rules;

    private void Start()
    {
        hands.InspectorInit(DeckType.hands);
        field.InspectorInit(DeckType.field);
        disCard.InspectorInit(DeckType.discard);
        trace.InspectorInit(DeckType.trace);
        senter.InspectorInit(DeckType.deck);
        right.InspectorInit(DeckType.right);
        left.InspectorInit(DeckType.left);
    }

    public Deck DeckKey(DeckType e)
    {
        if (e == DeckType.hands) return hands;
        if (e == DeckType.field) return field;
        if (e == DeckType.discard) return disCard;
        if (e == DeckType.trace) return trace;
        if (e == DeckType.deck) return senter;
        if (e == DeckType.right) return right;
        if (e == DeckType.left) return left;

        return null;

    }

}

public enum DeckType
{
    hands, field, discard, trace, deck, right, left
}
