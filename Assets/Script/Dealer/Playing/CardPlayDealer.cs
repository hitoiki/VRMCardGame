using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardPlayDealer : MonoBehaviour
{
    //CardSkillをStackに入れて順次実行するクラス

    [SerializeField] private StateDealer state;
    [SerializeField] private string skillingState;

    Queue<SkillProcess> skillQueue = new Queue<SkillProcess>();
    [SerializeField] CardFacade facade;

    public void SkillPush(List<SkillProcess> skills)
    {
        if (!skills.Any()) return;
        Debug.Log("Pushed");
        foreach (SkillProcess s in skills)
        {
            skillQueue.Enqueue(s);
        }
    }

    public void SkillExecute()
    {
        if (!skillQueue.Any())
        {
            Debug.Log("nulled");
            return;
        }
        int SkillCount = 0;
        while (skillQueue.Any())
        {
            Debug.Log("skilling");
            skillQueue.Dequeue().skill(facade);
            Debug.Log("skilled");
            SkillCount++;
            if (SkillCount > 99)
            {
                Debug.Log("OverFlow!!!!");
                break;
            }
        }
    }

    public void CardPlay(List<SkillProcess> skills)
    {
        SkillPush(skills);
        SkillExecute();
        //state.ChangeState(skillingState);
    }
}
