using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "memo", menuName = "ObjectAddress/Effect")]
public class EffectUsingObjectAddress : ScriptableObject
{
    // ここにScene中のゲームオブジェクトを代入して後から参照する
    // Effect用

    public ObjectFlyer<CoinSprite> flyer;
    [SerializeField] CoinSprite initCoinSprite;

    public void Init()
    {
        flyer = new ObjectFlyer<CoinSprite>(initCoinSprite);
    }
}
