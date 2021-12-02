using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject upbound;
    public GameObject lowbound;
    Vector3 back;
    Vector3 forth; 
    float phase = 0;
    float speed = 0.05f; 
    float phaseDirection = 1; 

    void Update()
    {
        back = upbound.transform.position;
        forth = lowbound.transform.position;
        transform.position = Vector3.Lerp(back, forth, phase); 
        phase += Time.deltaTime * speed * phaseDirection;
        if (phase >= 1 || phase <= 0) phaseDirection *= -1; 
    }
}
