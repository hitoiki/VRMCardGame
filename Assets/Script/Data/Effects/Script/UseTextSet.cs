using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
[CreateAssetMenu(fileName = "Data", menuName = "CardText/TextSet/UseTextSet")]
public class UseTextSet : UseText
{
    [SerializeField] private UseText[] texts;

    public override string Text(){
        string str = "";
        foreach (UseText t in texts)
        {
            str += "\n"+t.Text();
        }
        return str;
    }
    public override void Effect(CardDealer dealer, Card target){
        foreach (UseText t in texts) t.Effect(dealer, target);
    }
}
