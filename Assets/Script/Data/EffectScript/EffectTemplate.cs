using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[CreateAssetMenu(fileName = "Template", menuName = "EffectTemplate")]
public class EffectTemplate : ScriptableObject
{
    [SerializeReference, SubclassSelector] public ISkillEffect[] effect;
}
