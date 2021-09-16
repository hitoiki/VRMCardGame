using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grid
{
    //マス目、番目を計算するクラス
    [SerializeField] private Vector3 originPos;
    [SerializeField] private float distanceX;
    [SerializeField] private float distanceY;
    [SerializeField] private int maxXGrid;
    public Grid(Vector3 origin, float x, float y)
    {
        originPos = origin;
        distanceX = x;
        distanceY = y;
    }

    public Vector3 NumberGrid(int x)
    {
        if (maxXGrid > 0) return originPos + (x % maxXGrid) * distanceX * Vector3.right + (x / maxXGrid) * distanceY * Vector3.up;
        else return originPos + x * distanceX * Vector3.right;
    }

    public Vector3 Point(int x, int y)
    {
        return originPos + x * distanceX * Vector3.right + y * distanceY * Vector3.up;
    }
}
