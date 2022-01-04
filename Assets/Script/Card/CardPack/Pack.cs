using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pack", menuName = "Pack")]
public class Pack : ScriptableObject
{
    //今後大量に現れるであろうパックとしてのカードをいつか実装するぞ
    //ScriptableObjectで取る事で色々参照しやすくする
    [SerializeField] public string textName;
    [SerializeField] private List<CardData> cards;

    public List<CardData> GetCards()
    {
        return cards;
    }

}
