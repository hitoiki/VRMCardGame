using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDrawProcess : ISkillText
{
    //Deck間を移動した時に発動する能力。
    SkillProcess GetProcess(StageDeck from, StageDeck to);
    IsSkillable GetIsSkillable(StageDeck from, StageDeck to);
}