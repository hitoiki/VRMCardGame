using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckEditor : MonoBehaviour
{
    //Deckに代入してくれます
    public Deck deck;
    public List<CardData> cards;
    public bool alwaysCopy;
    public bool alwaysSubstitution;
    [ContextMenu("Substitution")]
    private void Substitution()
    {
        if (deck != null) deck.SubstitutionCard(cards);
    }
    [ContextMenu("Copy")]
    private void Copy()
    {
        cards = deck.cards.Select(x => { return x.GetCardData(); }).ToList();
    }

    private void Update()
    {
        if (alwaysCopy && alwaysSubstitution)
        {
            Debug.Log("片方だけ付けてくださいな");
            alwaysCopy = false;
            alwaysSubstitution = false;
        }
        if (alwaysCopy) Copy();
        if (alwaysSubstitution) Substitution();
    }

}
