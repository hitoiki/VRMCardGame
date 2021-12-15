using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddressSetter : MonoBehaviour
{
    [SerializeField] SkillUsingObjectAddress address;
    [SerializeField] AudioSource audioSource;
    [SerializeField] public CardSelector selector;
    private void Start()
    {
        address.source = audioSource;
        address.selector = selector;
    }
}
