using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class MovingEffect : SkillEffect
{
    [SerializeField] GameObject flyingObj;
    [SerializeField] float tweenTime;
    public override void Effect(IDealableCard Source, IDealableCard[] Target)
    {
        Debug.Log("Effected");
        this.transform.position = Source.GetTransform().position;
        if (Target != null && Target.Any())
        {
            foreach (Vector3 pos in Target.Select(x => { return x.GetTransform().position; }))
            {
                GameObject copy = Instantiate(flyingObj, this.transform.position, Quaternion.identity);
                copy.transform.DOMove(pos, tweenTime).OnComplete(() => { Destroy(copy); });

            }

        }
        Destroy(this.gameObject);
    }
}
