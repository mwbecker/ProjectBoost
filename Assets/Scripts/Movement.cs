using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;

    AudioSource audioSource;
    [SerializeField] float mainThrust = 100;
    [SerializeField] float rotationThrust = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isThrusting = ProcessThrust();
        if (audioSource.isPlaying && !isThrusting) audioSource.Stop();
        ProcessRotation();
    }

    bool ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) )
        {
            if (!audioSource.isPlaying) audioSource.Play();
            rb.AddRelativeForce(mainThrust * Vector3.up * Time.deltaTime );
            return true;
        }
        else {
            return false;
        }
       
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
           ApplyRotation(rotationThrust);            
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freeze so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing rotation 
    }
}
