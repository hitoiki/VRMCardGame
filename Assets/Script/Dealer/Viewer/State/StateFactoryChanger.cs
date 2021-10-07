using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StateFactoryChanger : MonoBehaviour, IGameState
{
    [SerializeField] private GameObject initFactory;
    private ICardFactory factory;

    private void OnValidate()
    {
        if (initFactory == null || initFactory.GetComponent<ICardFactory>() == null) initFactory = null;
    }

    private void Awake()
    {
        factory = initFactory.GetComponent<ICardFactory>();
    }
    public void CrankIn()
    {
    }

    public void StateUpdate()
    {

    }

    public void CrankUp()
    {

    }
}
