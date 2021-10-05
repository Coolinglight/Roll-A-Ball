using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotationAngle;
    public float speed = 1;
    void Update()
    {
        transform.Rotate(rotationAngle * Time.deltaTime * speed);
    }
}
