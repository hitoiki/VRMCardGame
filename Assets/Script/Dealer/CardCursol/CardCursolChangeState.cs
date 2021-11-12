using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardCursolChangeState : MonoBehaviour, IGameState
{
    // Stateに応じてICardCursolEventUserのICardCursolEventを書き換える
    //正直名前が長すぎる感じはする
    [SerializeField] private List<GameObject> initUsers = new List<GameObject>();
    private List<ICardCursolEventUser> users = new List<ICardCursolEventUser>();
    [SerializeReference, SubclassSelector] private List<ICardCursolEvent> events = new List<ICardCursolEvent>();
    private void OnValidate()
    {
        if (initUsers != null) initUsers = initUsers.Where(x => { return (x == null) || (x.GetComponent<ICardCursolEventUser>() != null); }).ToList();
    }
    private void Start()
    {
        users = initUsers.SelectMany(x => { return x.GetComponents<ICardCursolEventUser>(); }).ToList();
    }
    public void CrankIn()
    {
        foreach (ICardCursolEventUser u in users)
        {
            u.SubstitutionCardCursolEvent(events);
        }
    }
    public void StateUpdate()
    {

    }
    public void CrankUp()
    {
        foreach (ICardCursolEventUser u in users)
        {
            u.SubstitutionCardCursolEvent(new List<ICardCursolEvent>());
        }
    }
}
