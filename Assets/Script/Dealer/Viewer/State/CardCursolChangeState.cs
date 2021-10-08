using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardCursolChangeState : MonoBehaviour, IGameState
{
    // Stateに応じてICardCursolEventUserのICardCursolEventを書き換える
    //正直名前が長すぎる感じはする
    [SerializeField] private List<GameObject> initUsers = new List<GameObject>();
    [SerializeField] private List<GameObject> initEvents = new List<GameObject>();
    private List<ICardCursolEventUser> users = new List<ICardCursolEventUser>();
    private List<ICardCursolEvent> events = new List<ICardCursolEvent>();
    [SerializeField] private bool substitutionMode;
    private void OnValidate()
    {
        if (initUsers != null) initUsers = initUsers.Where(x => { return (x == null) || (x.GetComponent<ICardCursolEventUser>() != null); }).ToList();
        if (initEvents != null) initEvents = initEvents.Where(x => { return (x == null) || (x.GetComponent<ICardCursolEvent>() != null); }).ToList();

    }
    private void Start()
    {
        users = initUsers.SelectMany(x => { return x.GetComponents<ICardCursolEventUser>(); }).ToList();
        events = initEvents.SelectMany(x => { return x.GetComponents<ICardCursolEvent>(); }).ToList();
    }
    public void CrankIn()
    {
        //ネスト深いけど許して
        foreach (ICardCursolEventUser u in users)
        {
            if (substitutionMode) u.SubstitutionCardCursolEvent(events);
            else
            {
                foreach (ICardCursolEvent e in events)
                {
                    u.AddCardCursolEvent(e);
                }
            }
        }
    }
    public void StateUpdate()
    {

    }
    public void CrankUp()
    {
        if (!substitutionMode)
        {
            foreach (ICardCursolEventUser u in users)
            {

                foreach (ICardCursolEvent e in events)
                {
                    u.RemoveCardCursolEvent(e);
                }
            }
        }
    }
}
