using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    //マス目、番目を計算するクラス
    private Vector3 originPos;
    private float distanceX;
    private float distanceY;
    public Grid(Vector3 origin, float x, float y)
    {
        originPos = origin;
        distanceX = x;
        distanceY = y;
    }
}
