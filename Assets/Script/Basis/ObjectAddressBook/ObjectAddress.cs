using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "memo", menuName = "ObjectAddress")]
public class ObjectAddress : ScriptableObject
{
    //ここにシーン中のAudioSourceを置いて外から参照する
    [SerializeField] public AudioSource source;
}
