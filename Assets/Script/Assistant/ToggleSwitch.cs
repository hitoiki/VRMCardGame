using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

#pragma warning disable 0649
public class ToggleSwitch : MonoBehaviour, ICursolable
{
    // 押して当たり判定が変化するボタン（Collider）

    public ReactiveProperty<bool> toggle = new ReactiveProperty<bool>(false);
    [SerializeField] private BoxCollider col;
    [SerializeField] private float easingTime;
    [SerializeField] private Vector2 displayScale;
    [SerializeField] private Vector2 anchorScale;
    [SerializeField] private Vector2 displayPoint;
    [SerializeField] private Vector2 anchorPoint;

    public void Click(Vector3 point, ContactMode timing)
    {
        if (timing == ContactMode.Enter)
        {
            if (toggle.Value)
            {
                col.size = new Vector3(displayScale.x, displayScale.y, 1);
                this.transform.DOMove(displayPoint, easingTime);
            }
            else
            {
                col.size = new Vector3(anchorScale.x, anchorScale.y, 1);
                this.transform.DOMove(anchorPoint, easingTime);
            }
            toggle.Value = !toggle.Value;
        }
    }
    public void Cursol(Vector3 point, ContactMode mode)
    {

    }
}
