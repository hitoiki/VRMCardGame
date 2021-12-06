using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDrawProcess : ISkillText
{
    //Deck間を移動した時に発動する能力。
    void GetSkillProcess(CardFacade facade, StageDeck from, StageDeck to, DeckMove moveMode);
    bool GetIsSkillable(CardFacade facade, StageDeck from, StageDeck to, DeckMove moveMode);
}

public enum DeckMove
{
    Stay, Exit
}