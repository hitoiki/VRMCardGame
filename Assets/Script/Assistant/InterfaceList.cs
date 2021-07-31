using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InterfaceList<T>
{
    //interFaceを何やかんやするリスト
    //なんとか纏めようと思ったがなんか無理っぽいのでいちいちコレをコピペしてください

    [SerializeField] public List<GameObject> Initforrower = new List<GameObject>(1);
    public List<T> follower = null;
    private void OnValidate()
    {
        if (Initforrower != null) Initforrower = Initforrower.Where(x => { return (x == null) || (x.GetComponent<T>() != null); }).ToList();
    }
    private void Start()
    {
        follower = Initforrower.SelectMany(x => { return x.GetComponents<T>(); }).ToList();
    }

}
