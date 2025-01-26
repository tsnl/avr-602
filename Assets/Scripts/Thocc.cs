using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thocc : MonoBehaviour
{
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        // foreach (ContactPoint contact in collision.contacts)
        // {
        //     Debug.DrawRay(contact.point, contact.normal, Color.white);
        // }
        if (collision.relativeVelocity.magnitude > 2)
        {
            audioSource.Play();
        }
    }
}
