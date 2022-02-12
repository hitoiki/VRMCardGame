using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardSelectHighLighter : MonoBehaviour
{
    [SerializeField] private GameObject initFieldFactory;
    [SerializeField] private GameObject initHandFactory;
    [SerializeField] private Transform viewingObject;
    private ICardPrintableFactory fieldFactory;
    private ICardPrintableFactory handFactory;
    private ObjectFlyer<Transform> viewFlyer;

    private void OnValidate()
    {
        if (initFieldFactory == null || initFieldFactory.GetComponent<ICardPrintableFactory>() == null) initFieldFactory = null;
        if (initHandFactory == null || initHandFactory.GetComponent<ICardPrintableFactory>() == null) initHandFactory = null;
    }

    private void Awake()
    {
        fieldFactory = initFieldFactory.GetComponent<ICardPrintableFactory>();
        handFactory = initHandFactory.GetComponent<ICardPrintableFactory>();
        viewFlyer = new ObjectFlyer<Transform>(viewingObject.transform);
    }

    public void HandHighLight(ISkillBool condition)
    {
        IEnumerable<Vector3> positions = new List<Vector3>();

        if (condition == null)
        {
            positions = fieldFactory.GetCards().Select(x => { return x.GetTransform().position; });
        }

        if (condition != null)
        {
            positions = handFactory.GetCards()
                   .Where(x => { return condition.SkillBool(x.GetCard()); })
                   .Select(x => { return x.GetTransform().position; });
        }

        foreach (Vector3 t in positions)
        {
            viewFlyer.GetMob(t);
        }
    }

    public void FieldHighLight(ISkillBool condition)
    {

        IEnumerable<Vector3> positions = new List<Vector3>();

        if (condition == null)
        {
            positions = fieldFactory.GetCards().Select(x => { return x.GetTransform().position; });
        }

        if (condition != null)
        {
            positions = fieldFactory.GetCards().Where(x => { return condition.SkillBool(x.GetCard()); })
                .Select(x => { return x.GetTransform().position; });
        }

        foreach (Vector3 t in positions)
        {
            viewFlyer.GetMob(t);
        }
    }

    public void Erace()
    {
        viewFlyer.Erace();
    }

}
