using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddressSetter : MonoBehaviour
{
    [SerializeField] SkillUsingObjectAddress skillAddress;
    [SerializeField] EffectUsingObjectAddress effectAddress;
    [SerializeField] AudioSource audioSource;
    [SerializeField] public CardSelector selector;
    [SerializeField] EffectStateLinker linker;
    private void Start()
    {
        effectAddress.Init();
        skillAddress.source = audioSource;
        skillAddress.selector = selector;
        skillAddress.linker = linker;
    }
}
