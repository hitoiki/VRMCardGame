using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDemoChanger : MonoBehaviour
{
    [SerializeField] StateDealer dealer;
    [SerializeField] string statename;
    [ContextMenu("Change")]
    void Change()
    {
        dealer.ChangeState(statename);
    }
}
