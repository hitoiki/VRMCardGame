using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckEditor : MonoBehaviour
{
    //Deckに代入してくれます
    public Deck deck;
    public List<Card> cards;
    public bool alwaysCopy;
    public bool alwaysSubstitution;
    [ContextMenu("Substitution")]
    private void Substitution()
    {
        if (deck != null) deck.Substitution(cards);
    }
    [ContextMenu("Copy")]
    private void Copy()
    {
        cards = deck.cards;
    }

    private void Update()
    {
        if(alwaysCopy && alwaysSubstitution){
            Debug.Log("片方だけ付けてくださいな");
            alwaysCopy = false;
            alwaysSubstitution = false;
        }
        if (alwaysCopy) Copy();
        if (alwaysSubstitution) Substitution();
    }

}
