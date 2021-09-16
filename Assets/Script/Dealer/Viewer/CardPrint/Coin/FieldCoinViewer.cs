using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCoinViewer : MonoBehaviour
{
    [SerializeField] private CoinSprite coinSprite;
    [SerializeField] Stage stage;
    private ObjectFlyer<CoinSprite> flyer;

    private void Awake()
    {
        flyer = new ObjectFlyer<CoinSprite>(coinSprite);
    }


}
