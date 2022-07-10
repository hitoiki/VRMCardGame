using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class IroiroViewUI : MonoBehaviour
{
    [SerializeField] PlayerData player;
    [SerializeField] Text text;

    string money;
    string actionTime;
    string turn;
    private void Start()
    {
        player.instantMoney.Subscribe(x =>
        {
            money = "金:" + x.ToString();
            text.text = money + " " + actionTime;
        });
        player.actionTimes.Subscribe(x =>
        {
            actionTime = "アクション回数:" + x.ToString();
            text.text = money + " " + actionTime;
        });

    }
}
