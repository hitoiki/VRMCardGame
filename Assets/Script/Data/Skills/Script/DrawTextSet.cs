using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
[CreateAssetMenu(fileName = "Data", menuName = "CardText/TextSet/DrawTextSet")]
public class DrawTextSet : DrawText
{
    [SerializeField] private DrawText[] texts;

    public override string Text()
    {
        string str = "";
        foreach (DrawText t in texts)
        {
            str += "\n" + t.Text();
        }
        return str;
    }
    public override void Skill(CardDealer dealer, Card target, StageDeck from, StageDeck to)
    {
        foreach (DrawText t in texts) t.Skill(dealer, target, from, to);
    }
}
