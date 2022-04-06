using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectFlyer<T> where T : Component
{
    //一種類毎に扱う感じ
    //コイツを介してObject生成をする事で、処理が軽くなる魔法のコード
    public T DealMob;
    private List<T> _MobList;
    public IReadOnlyList<T> MobList => _MobList;

    public delegate void GeneTask(T t);

    public ObjectFlyer(T mob)
    {
        DealMob = mob;
        _MobList = new List<T>();
    }

    //Objectを渡す。
    public T GetMob(Vector3 pos)
    {
        foreach (var obj in _MobList)
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


        T newMob = GameObject.Instantiate(DealMob, pos, Quaternion.identity) as T;
        _MobList.Add(newMob);
        return newMob;
    }

    public T GetMob(Vector3 pos, GeneTask init)
    {
        foreach (var obj in _MobList)
        {
            if (obj.gameObject.activeSelf == false)
            {
                obj.transform.position = pos;
                obj.gameObject.SetActive(true);

                return obj;
            }
        }

        T newMob = GameObject.Instantiate(DealMob, pos, Quaternion.identity) as T;
        init(newMob);
        _MobList.Add(newMob);
        return newMob;
    }

    public T GetMob(Vector3 pos, GeneTask init, GeneTask calling)
    {
        foreach (var obj in _MobList)
        {
            if (obj.gameObject.activeSelf == false)
            {
                //Debug.Log("aa");
                obj.transform.position = pos;
                obj.gameObject.SetActive(true);
                calling(obj);
                return obj;
            }
        }

        T newMob = GameObject.Instantiate(DealMob, pos, Quaternion.identity) as T;
        init(newMob);
        _MobList.Add(newMob);
        return newMob;
    }

    public void Erace()
    {
        foreach (T obj in _MobList)
        {
            Transform.Destroy(obj.gameObject);
        }
        _MobList = new List<T>();
    }

    public void Release()
    {
        foreach (T obj in _MobList)
        {
            if (obj.gameObject.activeSelf == false)
            {
                Transform.Destroy(obj.gameObject);
                _MobList.Remove(obj);
            }
        }
    }
}

