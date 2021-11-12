using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class PlayerHPView : MonoBehaviour
{
    [SerializeField] PlayerData player;
    [SerializeField] Text text;
    private void Start()
    {
        player.hp.Subscribe(x =>
        {
            text.text = "HP:" + x.ToString();
        });
    }


}
