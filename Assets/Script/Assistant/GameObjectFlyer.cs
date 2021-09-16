using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFlyer
{
    //一種類毎に扱う感じ
    //コイツを介してObject生成をする事で、処理が軽くなる魔法のコード
    //GameObject用も一応作った
    public GameObject DealMob;
    private List<GameObject> MobList;

    public delegate void GeneTask(GameObject t);

    public GameObjectFlyer(GameObject mob)
    {
        DealMob = mob;
        MobList = new List<GameObject>();
    }

    //Objectを渡す。
    public GameObject GetMob(Vector3 pos)
    {
        foreach (var obj in MobList)
        {
            //Debug.Log(obj.name + ";" + obj.gameObject.activeSelf.ToString());
            if (obj.activeSelf == false)
            {
                //Debug.Log("aa");
                obj.transform.position = pos;
                obj.SetActive(true);
                return obj;
            }
        }


        GameObject newMob = DealMob;
        MobList.Add(MonoBehaviour.Instantiate(newMob, pos, Quaternion.identity));
        return newMob;
    }

    public GameObject GetMob(Vector3 pos, GeneTask init)
    {
        foreach (var obj in MobList)
        {
            //Debug.Log(obj.name + ";" + obj.gameObject.activeSelf.ToString());
            if (obj.activeSelf == false)
            {
                //Debug.Log("aa");
                obj.transform.position = pos;
                obj.SetActive(true);

                return obj;
            }
        }

        var newMob = MonoBehaviour.Instantiate(DealMob, pos, Quaternion.identity);
        init(newMob);
        MobList.Add(newMob);
        return newMob;
    }

    public GameObject GetMob(Vector3 pos, GeneTask init, GeneTask calling)
    {
        foreach (var obj in MobList)
        {
            //Debug.Log(obj.name + ";" + obj.gameObject.activeSelf.ToString());
            if (obj.activeSelf == false)
            {
                //Debug.Log("aa");
                obj.transform.position = pos;
                obj.SetActive(true);
                calling(obj);
                return obj;
            }
        }

        GameObject newMob = MonoBehaviour.Instantiate(DealMob, pos, Quaternion.identity);
        init(newMob);
        MobList.Add(newMob);
        return newMob;
    }

    public void Release()
    {
        foreach (var obj in MobList)
        {
            if (obj.activeSelf == false)
            {
                GameObject.Destroy(obj);
            }

        }
    }
}

