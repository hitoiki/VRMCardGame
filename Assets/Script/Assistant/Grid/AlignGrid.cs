using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AlignGrid : IAlignGrid
{
    //マス目、番目を計算するクラス
    [SerializeField] private Vector3 originPos;
    [SerializeField] private float distanceX;
    [SerializeField] private float distanceY;
    [SerializeField] private float avoidDistance;
    [SerializeField] private int maxXGrid;
    public int highLightingIndex;

    public Vector3 NumberGrid(int x)
    {
        Vector3 pos = new Vector3();
        if (maxXGrid > 0)
        {
            pos = originPos + (x % maxXGrid) * distanceX * Vector3.right + (x / maxXGrid) * distanceY * Vector3.up;
        }
        else
        {
            pos = originPos + x * distanceX * Vector3.right;
        }

        if (x != highLightingIndex && highLightingIndex >= 0) pos += avoidDistance / (highLightingIndex - x) * Vector3.left;
        return pos;
    }

    public void HighLightIndex(int x)
    {
        highLightingIndex = x;
    }
}
