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
    public Grid(Vector3 origin, float x, float y)
    {
        originPos = origin;
        distanceX = x;
        distanceY = y;
    }

    public Vector3 Point(int x, int y)
    {
        return originPos + x * distanceX * Vector3.right + y * distanceY * Vector3.up;
    }
}
