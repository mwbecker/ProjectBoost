using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
        if (Input.GetKey(KeyCode.L) ) nextLev();

        bool isThrusting = ProcessThrust();
        if (audioSource.isPlaying && !isThrusting)
        {
            StopThrusting();
        }
        ProcessRotation();
    }


    private void StopThrusting()
    {
        audioSource.Stop();
        mainThruster.Stop();
    }

    bool ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) )
        {
            return StartThrusting();
        }
        else {
            return false;
        }
       
    }

    private bool StartThrusting()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
            mainThruster.Play();
        }
        rb.AddRelativeForce(mainThrust * Vector3.up * Time.deltaTime);
        return true;
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rotateLeftLogic();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rotateRightLogic();
        }
        else
        {
            stopSideThrusting();
        }
    }

    private void stopSideThrusting()
    {
        leftThruster.Stop();
        rightThruster.Stop();
    }

    private void rotateRightLogic()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThruster.isPlaying)
            leftThruster.Play();
    }

    private void rotateLeftLogic()
    {
        ApplyRotation(rotationThrust);
        if (!rightThruster.isPlaying)
            rightThruster.Play();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freeze so we can manually rotate
        transform.Rotate(Vector3.left * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing rotation 
    }

    private void nextLev()
   {
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      int nextSceneIndex = (currentSceneIndex + 1) % (SceneManager.sceneCountInBuildSettings);
      
      SceneManager.LoadScene(nextSceneIndex);
   }
}
