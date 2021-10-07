using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayRecepter : MonoBehaviour
{
    //カードの使用を取り扱う
    //Rayかなんかで受け取る
    [SerializeField] private CardPlayChecker checker;

    [SerializeField] private Vector2 areaFrom = Vector2.zero;
    [SerializeField] private Vector2 areaTo = Vector2.zero;

    public void CardPlayRecept(Vector3 pos, Card card)
    {

        if (areaCheck(pos)) checker.CardPlay(card);


    }
    private bool areaCheck(Vector3 pos)
    {
        //長方形の中にいるかを判定するif文
        return areaFrom.x < pos.x == pos.x < areaTo.x && areaFrom.y < pos.y == pos.y < areaTo.y;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(areaFrom.x, areaFrom.y, this.transform.position.z), new Vector3(areaFrom.x, areaTo.y, this.transform.position.z));
        Gizmos.DrawLine(new Vector3(areaFrom.x, areaFrom.y, this.transform.position.z), new Vector3(areaTo.x, areaFrom.y, this.transform.position.z));
        Gizmos.DrawLine(new Vector3(areaTo.x, areaTo.y, this.transform.position.z), new Vector3(areaFrom.x, areaTo.y, this.transform.position.z));
        Gizmos.DrawLine(new Vector3(areaTo.x, areaTo.y, this.transform.position.z), new Vector3(areaTo.x, areaFrom.y, this.transform.position.z));

    }

}
