using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FacadeData
{
    //CardFacadeを生成するための初期情報
    [SerializeField] public PlayerData player;
    [SerializeField] public Stage stage = null;
    [SerializeField] public SkillUsingSubject skillsSubject;
    public SkillQueue skillQueue => stage.queueObject;
}
