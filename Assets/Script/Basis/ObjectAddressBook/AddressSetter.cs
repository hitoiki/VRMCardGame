using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddressSetter : MonoBehaviour
{
    [SerializeField] ObjectAddress address;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        address.source = audioSource;
    }
}
