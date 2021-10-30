using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacadeData : MonoBehaviour
{
    //CardFacadeを生成するための初期情報
    [SerializeField] public PlayerData player;
    [SerializeField] public Stage stage = null;
    [SerializeField] public Coin coinToCost;
    [SerializeField] public SkillQueueObject skillQueue => stage.queueObject;

}
