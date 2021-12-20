using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacadeData : MonoBehaviour
{
    //CardFacadeを生成するための初期情報
    [SerializeField] public PlayerData player;
    [SerializeField] public Stage stage = null;
    [SerializeField] public Coin coinToCost;
    [SerializeField] public SkillUsingSubject skillsSubject;

    [SerializeField] private GameObject initFieldFactory;
    [SerializeField] private GameObject initHandFactory;
    public ICardFactory fieldFactory;
    public ICardFactory handFactory;
    public SkillQueueObject skillQueue => stage.queueObject;

    private void OnValidate()
    {
        if (initFieldFactory == null || initFieldFactory.GetComponent<ICardFactory>() == null) initFieldFactory = null;
        if (initHandFactory == null || initHandFactory.GetComponent<ICardFactory>() == null) initHandFactory = null;
    }

    private void Awake()
    {
        fieldFactory = initFieldFactory.GetComponent<ICardFactory>();
        handFactory = initHandFactory.GetComponent<ICardFactory>();
    }

}
