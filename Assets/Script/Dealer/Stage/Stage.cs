using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
#pragma warning disable 0649
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
        hands.InspectorInit();
        field.InspectorInit();
        disCard.InspectorInit();
        trace.InspectorInit();
        senter.InspectorInit();
        right.InspectorInit();
        left.InspectorInit();
    }

    public Deck DeckKey(DeckType e)
    {
        if (e == DeckType.hands) return hands;
        if (e == DeckType.field) return field;
        if (e == DeckType.discard) return disCard;
        if (e == DeckType.trace) return trace;
        if (e == DeckType.senter) return senter;
        if (e == DeckType.right) return right;
        if (e == DeckType.left) return left;

        return null;

    }

}

public enum DeckType
{
    hands, field, discard, trace, senter, right, left
}
