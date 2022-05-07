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

    [SerializeField] AudioClip mainEngine;
    
    [SerializeField] ParticleSystem mainThruster;
    [SerializeField] ParticleSystem leftThruster;

    [SerializeField] ParticleSystem rightThruster;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isThrusting = ProcessThrust();
        if (audioSource.isPlaying && !isThrusting) {
           audioSource.Stop();
           mainThruster.Stop();
        }
        ProcessRotation();
    }

    bool ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) )
        {
            if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
            mainThruster.Play();
            }
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
           if (!rightThruster.isPlaying)
           rightThruster.Play();       
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            if (!leftThruster.isPlaying)
            leftThruster.Play();
        }
        else 
        {
            leftThruster.Stop();
            rightThruster.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freeze so we can manually rotate
        transform.Rotate(Vector3.left * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing rotation 
    }
}
