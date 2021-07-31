using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectFlyer<T> where T : Component
{
    //一種類毎に扱う感じ
    //コイツを介してObject生成をする事で、処理が軽くなる魔法のコード
    public T DealMob;
    private List<T> MobList;

    public delegate void GeneTask(T t);

    public ObjectFlyer(T mob)
    {
        DealMob = mob;
        MobList = new List<T>();
    }

    //Objectを渡す。
    public T GetMob(Vector3 pos)
    {
        foreach (var obj in MobList)
        {
            //Debug.Log(obj.name + ";" + obj.gameObject.activeSelf.ToString());
            if (obj.gameObject.activeSelf == false)
            {
                //Debug.Log("aa");
                obj.transform.position = pos;
                obj.gameObject.SetActive(true);
                return obj;
            }
        }


        var newMob = DealMob as T;
        MobList.Add(MonoBehaviour.Instantiate(newMob, pos, Quaternion.identity));
        return newMob;
    }

    public T GetMob(Vector3 pos, GeneTask init)
    {
        foreach (var obj in MobList)
        {
            //Debug.Log(obj.name + ";" + obj.gameObject.activeSelf.ToString());
            if (obj.gameObject.activeSelf == false)
            {
                //Debug.Log("aa");
                obj.transform.position = pos;
                obj.gameObject.SetActive(true);

                return obj;
            }
        }

        var newMob = MonoBehaviour.Instantiate(DealMob, pos, Quaternion.identity) as T;
        init(newMob);
        MobList.Add(newMob);
        return newMob;
    }

    public T GetMob(Vector3 pos, GeneTask init, GeneTask calling)
    {
        foreach (var obj in MobList)
        {
            //Debug.Log(obj.name + ";" + obj.gameObject.activeSelf.ToString());
            if (obj.gameObject.activeSelf == false)
            {
                //Debug.Log("aa");
                obj.transform.position = pos;
                obj.gameObject.SetActive(true);
                calling(obj);
                return obj;
            }
        }

        var newMob = MonoBehaviour.Instantiate(DealMob, pos, Quaternion.identity) as T;
        init(newMob);
        MobList.Add(newMob);
        return newMob;
    }

    public void Release()
    {
        foreach (var obj in MobList)
        {
            if (obj.gameObject.activeSelf == false)
            {
                GameObject.Destroy(obj.gameObject);
            }

        }
    }
}

