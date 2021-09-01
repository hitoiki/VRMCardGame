using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class CoinTextSet : CoinText
{
    [SerializeField] private CoinText[] texts = null;

    public override string Text(){
        string str = "";
        foreach (CoinText t in texts)
        {
            str += "\n"+t.Text();
        }
        return str;
    }
    public override void Effect(CardDealer dealer, Card target, Coin c, short n){
        foreach (CoinText t in texts) t.Effect(dealer, target, c, n);
    }
}
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
public class DrawTextSet : DrawText
{
    [SerializeField] private DrawText[] texts;

    public override string Text(){
        string str = "";
        foreach (DrawText t in texts)
        {
            str += "\n"+t.Text();
        }
        return str;
    }
    public override void Effect(CardDealer dealer, Card target, StageDeck from, StageDeck to){
        foreach (DrawText t in texts) t.Effect(dealer, target, from, to);
    }
}
