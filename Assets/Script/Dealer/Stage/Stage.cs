using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Stage : MonoBehaviour
{
    //Cardを置く盤面、舞台
    //コイツを読んで処理したりしなかったりする
    //データ等は主にここで握る
    [SerializeReference, SubclassSelector] public IStagingDeck hands;
    [SerializeReference, SubclassSelector] public IStagingDeck field;
    [SerializeReference, SubclassSelector] public IStagingDeck disCard;
    [SerializeReference, SubclassSelector] public IStagingDeck trace;
    [SerializeReference, SubclassSelector] public IStagingDeck senter;
    [SerializeReference, SubclassSelector] public IStagingDeck right;
    [SerializeReference, SubclassSelector] public IStagingDeck left;

    public SkillQueue queueObject = new SkillQueue();

    private void Start()
    {
        if (hands != null) hands.Init(DeckType.hands);
        if (field != null) field.Init(DeckType.field);
        if (disCard != null) disCard.Init(DeckType.discard);
        if (trace != null) trace.Init(DeckType.trace);
        if (senter != null) senter.Init(DeckType.deck);
        if (right != null) right.Init(DeckType.right);
        if (left != null) left.Init(DeckType.left);
    }

    public IStagingDeck DeckKey(DeckType e)
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
