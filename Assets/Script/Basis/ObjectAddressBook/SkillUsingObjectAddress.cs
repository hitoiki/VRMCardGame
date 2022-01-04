using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "memo", menuName = "ObjectAddress/Skill")]
public class SkillUsingObjectAddress : ScriptableObject
{
    //ここにシーン中のAudioSourceを置いて外から参照する
    [SerializeField] public AudioSource source;
    //IRawからSelectorは選ばないこと
    [SerializeField] public CardSelector selector;
    [SerializeField] public EffectStateLinker linker;

}
