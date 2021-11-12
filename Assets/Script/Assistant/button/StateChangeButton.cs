using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StateChangeButton : MonoBehaviour
{
    [SerializeField] private StateDealer dealer;

    public void ChangeState(string changeStateName)
    {
        dealer.ChangeState(changeStateName);
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
