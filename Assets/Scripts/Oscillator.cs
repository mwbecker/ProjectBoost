using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPostion;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
     float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startingPostion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) return ;
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinF = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinF + 1f)/2;
        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingPostion;
    }
}
