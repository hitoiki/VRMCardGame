using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Coin", menuName = "Coin")]
public class Coin : ScriptableObject
{
    //ダメージ、炎など、カードが受け取る効果量を示すクラス
    //カウンターになぞらえてコインと呼ばさせていただく
    [SerializeField] public string TextName;
    [SerializeField] public Sprite icon;
    [SerializeField] public Vector3 spritePos;
    [SerializeField] public float spriteScale;

    // Dictionaryで使うためにイコールをオーバーライド
    //名前だけで判断しているのはご愛嬌

    public override bool Equals(object obj)
    {
        var item = obj as Coin;

        if (obj == null)
        {
            return false;
        }

        return name == item.name;
        throw new System.NotImplementedException();
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        return name.GetHashCode();
        throw new System.NotImplementedException();
    }

}